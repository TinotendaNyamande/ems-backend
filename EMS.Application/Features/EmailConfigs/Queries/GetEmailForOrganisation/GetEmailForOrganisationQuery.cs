using EMS.Application.Dtos.EmailConfigs;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmailForOrganisation
{
    public class GetEmailForOrganisationQuery(Guid organisationId):IRequest<IEnumerable<EmailConfigDto>>
    {
        public Guid OrganisationId { get; init; } = organisationId;
    }
}
