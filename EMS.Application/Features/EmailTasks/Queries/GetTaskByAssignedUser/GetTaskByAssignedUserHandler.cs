using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAssignedUser
{
    public class GetTaskByAssignedUserHandler(IEmailTasksRepository tasksRepository) : ICommandHandler<GetTaskByAssignedUserQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetTaskByAssignedUserQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTasksByUserIdAsync(request.UserId, request.Status);
        }
    }
}
