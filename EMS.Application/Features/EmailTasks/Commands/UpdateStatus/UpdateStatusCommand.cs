using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    public record UpdateStatusCommand (Guid Id,TaskStatusList NewStatus,string? AdditionalInformation=null,Guid? OrganisationId=null):IRequest {}
}