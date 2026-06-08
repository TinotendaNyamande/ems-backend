using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.DeleteJoinRequest
{
    public class DeleteJoinRequestHandler(IJoinRequestsRepository joinRequests) : IRequestHandler<DeleteJoinRequestCommand>
    {
        public async Task Handle(DeleteJoinRequestCommand request, CancellationToken cancellationToken)
        {
            await joinRequests.DeleteRequestAsync(request.RequestId);
        }
    }
}
