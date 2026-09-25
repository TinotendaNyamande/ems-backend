using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.CreateTask
{
    internal class CreateTaskHandler(IEmailTasksRepository tasksRepository) : ICommandHandler<CreateTaskCommand,EmailTask>
    {
        public async Task<EmailTask> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = new EmailTask(command.EmailId, command.AssignedToUser);
            return await tasksRepository.CreateTaskAsync(task);
        }
    }
}