using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.CreateTask
{
    internal class CreateTaskHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<CreateTaskCommand>
    {
        public async Task Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = new EmailTask(command.EmailId, command.AssignedToUser);
            await tasksRepository.CreateTaskAsync(task);
        }
    }
}