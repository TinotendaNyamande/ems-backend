using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllTasks
{
    public record GetAllTasksQuery():IQuery<IEnumerable<GetTasksDto>> {};
}