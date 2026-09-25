using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAssignedUser
{
    public class GetTaskByAssignedUserQuery(string userId, TaskStatusList? status = null) : ICommand<IEnumerable<GetTasksDto>>
    {
        public string UserId { get; init; } = userId;
        public TaskStatusList? Status { get; init; } = status;
    }
}
