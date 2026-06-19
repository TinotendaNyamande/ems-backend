using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendTestEmailAsync(ValidationAndTestEmailAccountDto config, string toEmail);
    }
}
