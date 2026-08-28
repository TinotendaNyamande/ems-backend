using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;
using EMS.EmailCategorization.Worker.Models;
using Microsoft.Extensions.AI;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class EmailCategorizerService  (
        IChatClient chat,
         ILogger<EmailCategorizerService> logger,
         IEmailRepository emailRepository,
         IEmailCategoryRepository emailCategoryRepository
         ) : IEmailCategorizerService
    {
        private readonly IChatClient _chat = chat;
        private async Task<EmailCategoryResult> CategorizeAsync(
            string subject,
            string body,
            IEnumerable<string> categories,
            CancellationToken cancellationToken = default)
        {
            var categoryList = categories
                .Where(category => !string.IsNullOrWhiteSpace(category))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (categoryList.Length == 0)
            {
                throw new InvalidOperationException("At least one email category is required to classify an email.");
            }

            var prompt = $"""
            You are an email classification system.

            Choose exactly ONE category from the list below and return the exact category name.
            Categories:
            {string.Join(Environment.NewLine, categoryList.Select(category => $"- {category}"))}

            Subject:
            {subject}

            Body:
            {body}

            Return JSON with keys named category and reason.
            """;

            logger.LogInformation("Classifying email using {categoryCount} categories", categoryList.Length);

            var response = await _chat.GetResponseAsync<EmailCategoryResult>(
                prompt,
                cancellationToken: cancellationToken);

            if (!response.TryGetResult(out var result))
            {
                throw new InvalidOperationException("The AI model returned an invalid category response.");
            }

            var matchedCategory = categoryList.FirstOrDefault(category =>
                string.Equals(category, result.Category, StringComparison.OrdinalIgnoreCase));


            if (matchedCategory is null)
            {
                throw new InvalidOperationException(
                    $"The AI model returned an unknown category '{result.Category}'.");
            }

            result.Category = matchedCategory;
            logger.LogInformation("Result returned {result}", result.Category);
            return result;
        }
        public async Task ProcessAsync(EmailReceivedEvent message,CancellationToken cancellationToken)
        {
            logger.LogInformation("Start: Categorize email service");

                var categories =
                    await emailCategoryRepository
                        .GetEmailCategoriesAsync(message.OrganisationId);

                var categoryNames = categories
                    .Select(c => c.CategoryName)
                    .ToArray();

                    try
                    {
                        var result = await CategorizeAsync(
                            message.Subject,
                            message.Body,
                            categoryNames);

                        var matchedCategory = categories
                            .FirstOrDefault(c =>
                                c.CategoryName.Equals(
                                    result.Category,
                                    StringComparison.OrdinalIgnoreCase));

                        if (matchedCategory == null)
                        {
                            logger.LogWarning(
                                "No category match found for AI category: {Category}",
                                result.Category);

                            return;
                        }

                        await emailRepository.AssignNewEmailCategory(
                            message.EmailId,
                            matchedCategory.Id);

                        logger.LogInformation(
                            "Email {EmailId} categorized as {Category}",
                            message.EmailId,
                            matchedCategory.CategoryName);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Error categorizing email {EmailId}",
                            message.EmailId);
                    }


            
            logger.LogInformation("End: Categorize email service");}

    }
}
