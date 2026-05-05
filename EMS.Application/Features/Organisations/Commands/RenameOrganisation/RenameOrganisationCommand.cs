using MediatR;

namespace EMS.Application.Features.Organisations.Commands.RenameOrganisation
{
    public record RenameOrganisationCommand(Guid organisationId,string newOrganisationName):IRequest
    {
       public Guid OrganisationId { get; init; }=organisationId;
        public string OrganisationNewName { get; init; }=newOrganisationName;
    }
}
