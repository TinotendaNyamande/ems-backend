using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ValidateConfig
{
    public record ValidateConfigCommand(Guid id) :IRequest<bool>
    {
        public Guid Id { get; init; } = id;
    }
}
