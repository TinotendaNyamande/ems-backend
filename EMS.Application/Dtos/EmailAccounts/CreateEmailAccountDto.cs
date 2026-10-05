using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailAccounts
{
    public record CreateEmailAccountDto(string EmailAddress, EmailType EmailType, string? Password, string? ClientId, string? ClientSecret, string? TenantId);
}