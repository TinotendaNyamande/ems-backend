using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.TestEmail
{
    public record TestEmailCommand(Guid Id,string ToEmail) :IRequest
    {
    }
}
