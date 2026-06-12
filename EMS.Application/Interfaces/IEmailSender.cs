using EMS.Application.Dtos.EmailConfigs;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendTestEmailAsync(ValidationAndTestEmailDto config, string toEmail);
    }
}
