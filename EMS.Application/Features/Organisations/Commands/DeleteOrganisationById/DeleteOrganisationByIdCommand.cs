using MediatR;

namespace EMS.Application.Features.Organisations.Commands.DeleteOrganisationById
{
    public class DeleteOrganisationByIdCommand(Guid organisationId):IRequest
    {
        public Guid OrganisationId { get; init; }=organisationId;
    }
}
