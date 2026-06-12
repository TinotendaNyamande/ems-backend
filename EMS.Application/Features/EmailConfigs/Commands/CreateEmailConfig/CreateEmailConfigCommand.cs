using EMS.Application.Dtos.EmailConfigs;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig
{
    public record CreateEmailConfigCommand(
        string EmailAddress, EmailType EmailType,Guid OrganisationId,string? Password=null,string? ClientId=null,string? ClientSecret=null,string? TenantId=null):IRequest<EmailConfigDto>
    {
       
    }
}
