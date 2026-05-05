using MediatR;

namespace EMS.Application.Features.Organisations.Commands.ChangeOwner
{
    public record ChangeOwnerCommand(Guid organisationId,string newOwnerId):IRequest
    {
        public Guid OrganisationId { get; init; }=organisationId;
        public string NewOwnerId { get; init; } = newOwnerId;
    }
}
