using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.ReadEmailsFromInbox
{
    public record ReadEmailsFromInboxCommand(Guid Id ,string EmailAddress,EmailType EmailType, string? Password,string? ClientSecret,string? TenantId,string? ClientId):IRequest;
}