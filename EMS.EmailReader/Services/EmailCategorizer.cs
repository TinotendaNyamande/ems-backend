using EMS.EmailReader.Models;
using Microsoft.Extensions.AI;

namespace EMS.EmailReader.Services
{
    internal class EmailCategorizer(IChatClient chat, ILogger<EmailCategorizer> logger) : IEmailCategorizer
    {
        private readonly IChatClient _chat = chat;
        private readonly ILogger<EmailCategorizer> _logger = logger;

        public async Task<EmailCategoryResult> CategorizeAsync(
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
            _logger.LogInformation("Result returned {result}",result.Category);
            return result;
        }
    }
}
