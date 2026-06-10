using EMS.Application.Dtos.EmailConfigs;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig
{
    public record CreateEmailConfigCommand(string EmailAddress, EmailType EmailType,string Password,string ClientId,string ClientSecret,string TenantId,Guid OrganisationId):IRequest<EmailConfigDto>
    {
       
    }
}
