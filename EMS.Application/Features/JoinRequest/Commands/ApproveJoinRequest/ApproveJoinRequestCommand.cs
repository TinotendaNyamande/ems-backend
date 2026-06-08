using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.ApproveJoinRequest
{
    public record ApproveJoinRequestCommand(Guid RequestId, Guid OrganisationId, string ApprovingUserId,Guid RoleId) : IRequest
    {
    }
}
