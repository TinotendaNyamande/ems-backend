using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId
{
    public record GetAllOpenTasksByUserIdQuery(string UserId):IQuery<IEnumerable<GetTasksDto>> {};
}