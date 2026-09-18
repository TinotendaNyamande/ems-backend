using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class CategorizeEmails(IEmailCategoryRepository emailCategoryRepository,IEmailRepository emailRepository, IEmailCategorizerService emailCategorizerService, ILogger<CategorizeEmails> logger)
    :ICategorizeEmails
    {
        public async Task ProcessAsync(EmailReceivedEvent message,CancellationToken cancellationToken)
        {
            logger.LogInformation("Start: Categorize email service");

                var categories =
                    await emailCategoryRepository
                        .GetEmailCategoriesByEmailAccountAsync(message.EmailAccountId);

                var categoryNames = categories
                    .Select(c => c.CategoryName)
                    .ToArray();

                    try
                    {
                        var result = await emailCategorizerService.CategorizeAsync(
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