using EMS.Domain.Models;

namespace EMS.EmailReader.Services
{
    public interface IEmailReaderService
    {
        Task ReadGmailInboxService(EmailAccount emailAccount);
        Task ReadOffice365InboxService(EmailAccount emailAccount);
    }
}
