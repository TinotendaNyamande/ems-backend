using EMS.Application.Dtos.JoinRequests;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetUserJoinRequest
{
    internal class GetUserJoinRequestHandler(IJoinRequestsRepository joinRequestsRepository) : IRequestHandler<GetUserJoinRequestQuery, IEnumerable<JoinRequestDto>>
    {
        public async Task<IEnumerable<JoinRequestDto>> Handle(GetUserJoinRequestQuery request, CancellationToken cancellationToken)
        {
            return await joinRequestsRepository.GetUserJoinRequestsAsync(request.UserId);
        }
    }
}
