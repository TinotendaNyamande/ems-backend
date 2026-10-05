using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllTasks
{
    internal class GetAllTasksHandler(IEmailTasksRepository emailTasksRepository) : IQueryHandler<GetAllTasksQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await emailTasksRepository.GetAllTasksAsync();
        }
    }
}