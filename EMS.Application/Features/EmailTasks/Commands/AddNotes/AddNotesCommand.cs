using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.AddNotes
{
    public record AddNotesCommand(Guid Id,string AdditionalInfo,string UserId):IRequest{}
}