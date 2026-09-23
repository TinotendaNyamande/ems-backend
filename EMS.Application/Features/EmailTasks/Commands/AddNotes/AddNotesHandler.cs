using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.AddNotes
{
    internal class AddNotesHandler(IEmailTasksRepository tasksRepository,ITaskAuditRepository taskAuditRepository) : IRequestHandler<AddNotesCommand>
    {
        public async Task Handle(AddNotesCommand command,CancellationToken cancellation)
        {
            var audit = new TaskAuditTrail("Comment added",command.UserId,command.Id);
            await taskAuditRepository.CreateTaskAuditTrailAsync(audit);
            await tasksRepository.EditAdditionalInformationAsync(command.Id,command.AdditionalInfo);
        }
    }
}