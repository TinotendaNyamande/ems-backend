using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery(Guid id) : ICommand<GetTasksDto>
    {
        public Guid Id { get; init; } = id;
    }
}
