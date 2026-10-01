using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery(Guid id) : IQuery<GetTasksDetailsDto>
    {
        public Guid Id { get; init; } = id;
    }
}
