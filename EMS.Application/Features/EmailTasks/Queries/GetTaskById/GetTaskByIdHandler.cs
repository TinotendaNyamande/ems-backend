using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdHandler(IEmailTasksRepository tasksRepository) : ICommandHandler<GetTaskByIdQuery, GetTasksDto>
    {
        public async Task<GetTasksDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTaskByIdAsync(request.Id);
        }
    }
}
