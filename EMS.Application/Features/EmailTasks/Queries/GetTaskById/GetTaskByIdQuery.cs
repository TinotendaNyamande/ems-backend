using EMS.Application.Dtos.Tasks;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery(Guid id) : IRequest<GetTasksDto>
    {
        public Guid Id { get; init; } = id;
    }
}
