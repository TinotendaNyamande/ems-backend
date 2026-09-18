using System.Text.RegularExpressions;
using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;
using EMS.EmailCategorization.Worker.Models;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class RegexEmailCategorizerService(
        ) : IEmailCategorizerService
    {
        private const string FallbackCategory = "Other";

        private static string? TryMatch(string? text, string pattern)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            try
            {
                return Regex.IsMatch(
                    text,
                    pattern,
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant,
                    TimeSpan.FromMilliseconds(200))
                    ? pattern
                    : null;
            }
            catch (RegexParseException)
            {
                // A bad pattern should not crash the worker
                return null;
            }
        }

        /// <summary>
        /// Builds regex patterns for a category name.
        /// - Full name as a word-bounded phrase: "Email Marketing" -> \bemail\s+marketing\b
        /// - Each individual word (length >= 3) as its own pattern so "Invoice" matches "invoices".
        /// </summary>
        private static IEnumerable<string> BuildPatterns(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                yield break;
            }

            var trimmed = categoryName.Trim();
            var escaped = Regex.Escape(trimmed);

            // Full name as a single phrase (allows flexible whitespace)
            var phrasePattern = @"\b" +
                                escaped.Replace(@"\ ", @"\s+") +
                                @"\b";
            yield return phrasePattern;

            // Individual words (skip short/common ones)
            var words = trimmed
                .Split(new[] { ' ', '-', '_', '/', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length >= 3);

            foreach (var word in words)
            {
                var wEscaped = Regex.Escape(word);
                // \w* suffix lets "invoice" match "invoices", "invoiced", etc.
                yield return @"\b" + wEscaped + @"\w*\b";
            }
        }

        public Task<EmailCategoryResult> CategorizeAsync(
            string? subject,
            string? body,
            IEnumerable<string> categoryNames,
            CancellationToken cancellationToken
            )
        {
            // Deduplicate & keep deterministic order
            var categoriesList = categoryNames
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            // 1) Subject first — higher signal
            foreach (var name in categoriesList)
            {
                foreach (var pattern in BuildPatterns(name))
                {
                    var matched = TryMatch(subject, pattern);
                    if (matched is not null)
                    {
                        return  Task.FromResult(new EmailCategoryResult
                        {
                            Category=categoriesList.FirstOrDefault(category =>string.Equals(category, name, StringComparison.OrdinalIgnoreCase))
                        });
                    }
                }
            }

            // 2) Then body
            foreach (var name in categoriesList)
            {
                foreach (var pattern in BuildPatterns(name))
                {
                    var matched = TryMatch(body, pattern);
                    if (matched is not null)
                    {
                        return Task.FromResult(new EmailCategoryResult
                        {
                            Category = categoriesList.FirstOrDefault(category =>string.Equals(category, name, StringComparison.OrdinalIgnoreCase))
                        });
                    }
                }
            }

            return Task.FromResult(new EmailCategoryResult
            {
                Category = categoriesList.FirstOrDefault(category =>string.Equals(category, FallbackCategory, StringComparison.OrdinalIgnoreCase))
            });
        }


    }
}