using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasks
{
    public record GetAllOpenTasksQuery() :ICommand<IEnumerable<GetTasksDto>> {};
}