using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.Emails.Commands.CategorizeEmails
{
    internal class CategorizeEmailsHandler(IEmailCategorizerService emailCategorizer,IEmailCategoryRepository emailCategoryRepository,IEmailRepository emailRepository, ILogger<CategorizeEmailsHandler> logger) : ICommandHandler<CategorizeEmailsCommand>
    {
        public async Task Handle(CategorizeEmailsCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Start: Categorize email service");

              var categories = await emailCategoryRepository.GetEmailCategoriesByEmailAccountAsync(request.EmailAccountId);

            var categoryNames = categories
                .Select(c => c.CategoryName)
                .ToArray();

            try
            {
                var result = await emailCategorizer.CategorizeAsync(request.Subject, request.Body, categoryNames);

                var matchedCategory = categories
                    .FirstOrDefault(c =>
                        c.CategoryName.Equals(
                            result.Category,
                            StringComparison.OrdinalIgnoreCase));

                if (matchedCategory == null)
                {
                    logger.LogWarning(
                        "No category match found for  category: {Category}",
                        result.Category);

                    return;
                }
                await emailRepository.AssignEmailCategoryAsync(request.EmailId,matchedCategory.Id);

                logger.LogInformation(
                    "Email {EmailId} categorized as {Category}",
                    request.EmailId,
                    matchedCategory.CategoryName);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error categorizing email {EmailId}",
                    request.EmailId);
            }

            logger.LogInformation("End: Categorize email service");
        }
    }
}

