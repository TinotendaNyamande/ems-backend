using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.DeleteEmail
{
    public class DeleteEmailCommand(Guid emailId) : IRequest
    {
        public Guid EmailId { get; init; } = emailId;
    }
}
