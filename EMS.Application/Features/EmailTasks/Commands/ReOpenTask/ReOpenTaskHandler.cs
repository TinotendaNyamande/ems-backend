using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReOpenTask
{
    public class ReOpenTaskHandler(IEmailTasksRepository emailTasksRepository) : IRequestHandler<ReOpenTaskCommand>
    {


        public async Task Handle(ReOpenTaskCommand request, CancellationToken cancellationToken)
        {
            await emailTasksRepository.ReOpenTaskAsync(request.TaskId);
        }
    }
}