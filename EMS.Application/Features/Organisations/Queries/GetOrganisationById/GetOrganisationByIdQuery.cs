using EMS.Application.Dtos.Organisation;
using MediatR;

namespace EMS.Application.Features.Organisations.Queries.GetOrganisationById
{
    public class GetOrganisationByIdQuery(Guid organisationId):IRequest<OrganisationDetailsDto>
    {
        public Guid OrganisationId { get; init; }=organisationId;
    }
}
