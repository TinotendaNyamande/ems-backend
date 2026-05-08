using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendTestEmailAsync(MailBoxConfig config, string toEmail);
    }
}
