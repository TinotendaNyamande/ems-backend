using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    public record UpdateStatusCommand (Guid Id,TaskStatusList NewStatus,string UserId,string? AdditionalInformation=null):IRequest {}
}