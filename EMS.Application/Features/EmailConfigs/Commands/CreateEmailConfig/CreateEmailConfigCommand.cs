using EMS.Application.Dtos.EmailConfigs;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig
{
    public class CreateEmailConfigCommand(string emailAddress, EmailType emailType,string password,string clientId,string clientSecret,string tenantId,Guid organisationId):IRequest<EmailConfigDto>
    {
        public string EmailAddress { get; init; } = emailAddress;
        public EmailType EmailType { get;init; }= emailType;
        public string? Password { get;init; }= password;
        public string? ClientId { get;init; }= clientId;
        public string? ClientSecret { get;init; }= clientSecret;
        public string? TenantId { get; init; }= tenantId;
        public Guid OrganisationId { get; init; }= organisationId;
    }
}
