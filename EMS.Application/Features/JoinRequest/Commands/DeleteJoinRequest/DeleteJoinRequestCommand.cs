using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.DeleteJoinRequest
{
    public record DeleteJoinRequestCommand(Guid RequestId):IRequest
    {
    }
}
