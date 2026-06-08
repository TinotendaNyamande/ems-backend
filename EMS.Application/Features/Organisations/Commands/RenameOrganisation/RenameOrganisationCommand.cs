using MediatR;

namespace EMS.Application.Features.Organisations.Commands.RenameOrganisation
{
    public record RenameOrganisationCommand(Guid OrganisationId,string NewOrganisationName):IRequest
    {
    }
}
