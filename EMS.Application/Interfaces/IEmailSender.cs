using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendTestEmailAsync(EmailType emailType,string emailAddress,string? password,string? clientId,string? clientSecret,string? tenantId,string toEmail);
    }
}
