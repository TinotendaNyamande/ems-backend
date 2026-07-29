using EMS.Application.Interfaces;
using EMS.EmailCategorization.Worker.Models;
using EMS.EmailCategorization.Worker.Services;
using Microsoft.Extensions.AI;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class EmailCategoryProcessor(
        IChatClient chat,
         ILogger<EmailCategoryProcessor> logger,
         IEmailAccountRepository emailAccountRepository,
         IEmailRepository emailRepository,
         IOrganisationRepository organisationRepository,
         IEmailCategoryRepository emailCategoryRepository
         ) : IEmailCategorizerService
    {
        private readonly IChatClient _chat = chat;
        private readonly ILogger<EmailCategoryProcessor> _logger = logger;

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

            _logger.LogInformation("Classifying email using {categoryCount} categories", categoryList.Length);

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
            _logger.LogInformation("Result returned {result}", result.Category);
            return result;
        }
        public async Task<int> ProcessAsync(CancellationToken cancellationToken)
        {
            var tasksCount = 0;
            logger.LogInformation("Start: Categorize email service");

            var emailAccounts = await emailAccountRepository
                .GetAllValidatedAsync();

            logger.LogInformation(
                "Found {count} validated mailboxes",
                emailAccounts.Count());

            foreach (var emailAccount in emailAccounts)
            {
                var organisation =
                    await organisationRepository
                        .GetByIdAsync(emailAccount.OrganisationId);

                var categories =
                    await emailCategoryRepository
                        .GetEmailCategoriesAsync(organisation.Id);

                var categoryNames = categories
                    .Select(c => c.CategoryName)
                    .ToArray();

                var emails =
                    await emailRepository
                        .GetNewEmailsByEmailAccount(emailAccount.Id);

                var tasks = emails.Select(async email =>
                {
                    try
                    {
                        var result = await CategorizeAsync(
                            email.Subject,
                            email.Body,
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
                            email.Id,
                            matchedCategory.Id);

                        logger.LogInformation(
                            "Email {EmailId} categorized as {Category}",
                            email.Id,
                            matchedCategory.CategoryName);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Error categorizing email {EmailId}",
                            email.Id);
                    }
                });

                await Task.WhenAll(tasks);
                tasksCount += emails.Count();

            }
            return tasksCount;
        }

    }
}
