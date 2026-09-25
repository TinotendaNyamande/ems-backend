using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllTasks
{
    internal class GetAllTasksHandler(IEmailTasksRepository emailTasksRepository) : ICommandHandler<GetAllTasksQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await emailTasksRepository.GetAllTasksAsync();
        }
    }
}