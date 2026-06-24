using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.CloseTask
{
    public record CloseTaskCommand (Guid Id,string AdditionalInfo):IRequest{}
}