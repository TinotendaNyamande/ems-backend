using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Domain.Enums;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAccount
{
    public record GetTaskByAccountQuery(Guid EmailAccountId, TaskStatusList? Status = null) : IQuery<IEnumerable<GetTasksDto>>
    {
    }
}
