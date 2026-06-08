using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.CreateJoinRequest
{
    public record CreateJoinRequestCommand(string RequestById, Guid OrganisationId):IRequest
    {
    }
}
