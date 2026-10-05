using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailAccounts
{
    public record EmailAccountDto(Guid Id, string EmailAddress, EmailType EmailType, string? Password,DateTime CreatedAt);
   
    
}
