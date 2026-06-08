using EMS.Application.Dtos.JoinRequests;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetAllJoinRequests
{
    internal class GetAllJoinRequestsHandler(IJoinRequestsRepository joinRequests):IRequestHandler<GetAllJoinRequestsQuery, IEnumerable<JoinRequestDto>>
    {
        public async Task<IEnumerable<JoinRequestDto>> Handle(GetAllJoinRequestsQuery request, CancellationToken cancellationToken)
        {
            return await joinRequests.GetAllJoinRequestsAsync(request.OrganisationId);
        }
    }
}
