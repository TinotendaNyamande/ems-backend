using EMS.Application.Dtos.JoinRequests;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetAllJoinRequests
{
    public record GetAllJoinRequestsQuery(Guid OrganisationId) : IRequest<IEnumerable<JoinRequestDto>>
    {
    }
}
