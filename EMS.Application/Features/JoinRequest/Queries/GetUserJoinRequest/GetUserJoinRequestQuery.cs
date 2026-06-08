using EMS.Application.Dtos.JoinRequests;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Queries.GetUserJoinRequest
{
    public record GetUserJoinRequestQuery(string UserId):IRequest<IEnumerable<JoinRequestDto>>
    {
    }
}
