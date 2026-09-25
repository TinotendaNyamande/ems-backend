using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount
{
    public record CreateEmailAccountCommand(
        string EmailAddress, EmailType EmailType,string? Password=null,string? ClientId=null,string? ClientSecret=null,string? TenantId=null):ICommand<EmailAccountDto>
    {
       
    }
}
