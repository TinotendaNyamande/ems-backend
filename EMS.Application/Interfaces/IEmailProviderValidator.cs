using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailProviderValidator
    {
        EmailType EmailType { get; }
        Task<bool> IsEmailConfigValidAsync(ValidationAndTestEmailAccountDto emailAccountDto);
    }
}
