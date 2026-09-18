using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasks
{
    public class GetAllOpenTasksHandler(IEmailTasksRepository emailTasksRepository) : IRequestHandler<GetAllOpenTasksQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetAllOpenTasksQuery request, CancellationToken cancellationToken)
        {
            return await emailTasksRepository.GetAllOpenTasksAsync();
        }
    };
}