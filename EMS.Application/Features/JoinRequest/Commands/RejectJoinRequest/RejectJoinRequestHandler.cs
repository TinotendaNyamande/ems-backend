using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.RejectJoinRequest
{
    public class RejectJoinRequestHandler(IJoinRequestsRepository joinRequests,IUserService userService) : IRequestHandler<RejectJoinRequestCommand>
    {
        public async Task Handle(RejectJoinRequestCommand request, CancellationToken cancellationToken)
        {   
            var user = await userService.GetUserByIdAsync(request.RejectingUserId);
            if (user.OrganisationId != request.OrganisationId)
            {
                throw new UnauthorizedAccessException("User does not have permission to reject this join request.");
            }
            await joinRequests.RejectJoinRequestAsync(request.RequestId, request.RejectingUserId);
        }
    }
}
