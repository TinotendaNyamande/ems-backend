using MediatR;

namespace EMS.Application.Features.Organisations.Commands.ChangeOwner
{
    public record ChangeOwnerCommand(Guid OrganisationId,string NewOwnerId):IRequest
    {
    }
}
