using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.DeleteTask
{
    internal class DeleteTaskHandler(IEmailTasksRepository tasksRepository, ITaskAuditRepository taskAuditRepository) : ICommandHandler<DeleteTaskCommand>
    {
        public async Task Handle(DeleteTaskCommand command, CancellationToken token)
        {
            var audit = new TaskAuditTrail("Task deleted", command.UserId, command.Id);
            await taskAuditRepository.CreateTaskAuditTrailAsync(audit);
            await tasksRepository.DeleteTaskAsync(command.Id);
        }
    }
}