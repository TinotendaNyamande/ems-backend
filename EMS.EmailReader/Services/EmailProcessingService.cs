using EMS.Application.Interfaces;
using EMS.Domain.Enums;

namespace EMS.EmailReader.Services
{
    internal class EmailProcessingService(
        IEmailAccountRepository emailAccountRepository,
         IEmailCategorizer emailCategorizer,
         IEmailReaderService emailReaderService,
         IEmailAssignmentService emailAssignmentService,
         ILogger<EmailProcessingService> logger
         ) : IEmailProcessingService
    {

        public async Task TaskAssignmentService()
        {
         await emailAssignmentService.AssignEmailsAsync();
        }
        public async Task EmailCategoryService()
        {
            await emailCategorizer.DetermineEmailCategory();
        }



        public async Task EmailReaderService()
        {
            logger.LogInformation("Start: Read from email service");
            var emailAccounts = await emailAccountRepository.GetAllValidatedAsync();
            logger.LogInformation("Found {count} number of validated mailboxes", emailAccounts.Count());
            if (emailAccounts.Any())
            {
                foreach (var emailAccount in emailAccounts)
                {
                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);

                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);
                    if (emailAccount.EmailType == EmailType.Office365)
                    {
                        await emailReaderService.ReadOffice365InboxService(emailAccount);

                    }
                    else
                    {
                        await emailReaderService.ReadGmailInboxService(emailAccount);
                    }
                    logger.LogInformation("Finished processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);


                }
            }

        }
    }
}