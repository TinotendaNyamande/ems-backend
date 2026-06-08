using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.RejectJoinRequest
{
    public record RejectJoinRequestCommand(Guid RequestId, Guid OrganisationId, string RejectingUserId) : IRequest
    {
    }
}
