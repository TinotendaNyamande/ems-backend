using EMS.Application.Dtos.JoinRequests;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetPendingJoinRequests
{
    internal class GetPendingJoinRequestsHandler(IJoinRequestsRepository joinRequests) : IRequestHandler<GetPendingJoinRequestsQuery, IEnumerable<JoinRequestDto>>
    {
        public async Task<IEnumerable<JoinRequestDto>> Handle(GetPendingJoinRequestsQuery request, CancellationToken cancellationToken)
        {
            return await joinRequests.GetPendingJoinRequestsAsync(request.OrganisationId);
        }
    }
}
