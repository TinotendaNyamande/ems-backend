using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailAccounts
{
    public record EditEmailAccountDto(Guid Id, string EmailAddress, EmailType EmailType, string? Password);
}

 