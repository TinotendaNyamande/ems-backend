using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    internal class ReassignTaskHandler(IEmailTasksRepository tasksRepository,IUserService userService) : IRequestHandler<ReassignTaskCommand>
    {
        public async Task Handle(ReassignTaskCommand command, CancellationToken token)
        {
             await userService.GetUserByIdAsync(command.NewUserId);
            await tasksRepository.ReassignTaskAsync(command.Id, command.NewUserId);
        }
    }
}