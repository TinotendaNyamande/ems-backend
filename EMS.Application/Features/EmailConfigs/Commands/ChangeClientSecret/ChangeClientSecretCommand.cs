using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public record ChangeClientSecretCommand(Guid EmailId,string OldSecret,string NewSecret):IRequest
    {

    }
}
