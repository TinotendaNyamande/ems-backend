using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public record ChangeClientSecretCommand(Guid emailId,string oldSecret,string newSecret):IRequest
    {
        public Guid EmailId { get; init; } = emailId;
        public string OldSecret { get; init; } = oldSecret;
        public string NewSecret { get; init; } = newSecret;
    }
}
