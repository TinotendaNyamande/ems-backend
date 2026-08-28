using EMS.Application.Dtos.Tasks;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAccount
{
    public record GetTaskByAccountQuery(Guid EmailAccountId, TaskStatusList? Status = null) : IRequest<IEnumerable<GetTasksDto>>
    {
    }
}
