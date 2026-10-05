using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId
{
    public class GetAllOpenTasksByUserIdHandler(IEmailTasksRepository emailTasksRepository) : IQueryHandler<GetAllOpenTasksByUserIdQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetAllOpenTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await emailTasksRepository.GetAllOpenTasksByUserIdAsync(request.UserId);
        }
    };
}