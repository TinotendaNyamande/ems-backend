using EMS.Application.Dtos.JoinRequests;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetPendingJoinRequests
{
    public record GetPendingJoinRequestsQuery(Guid OrganisationId) : IRequest<IEnumerable<JoinRequestDto>>
    {
    }
}
