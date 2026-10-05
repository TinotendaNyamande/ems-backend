using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdHandler(IEmailTasksRepository tasksRepository) : IQueryHandler<GetTaskByIdQuery, GetTasksDetailsDto>
    {
        public async Task<GetTasksDetailsDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTaskByIdAsync(request.Id)?? throw new ResourceNotFoundException("Task", request.Id);
        }
    }
}
