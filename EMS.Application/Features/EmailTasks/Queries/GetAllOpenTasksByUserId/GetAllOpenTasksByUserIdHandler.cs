using EMS.Application.Dtos.Tasks;
using EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId
{
    public class GetAllOpenTasksByUserIdHandler(IEmailTasksRepository emailTasksRepository) : IRequestHandler<GetAllOpenTasksByUserIdQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetAllOpenTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await emailTasksRepository.GetAllOpenTasksByUserIdAsync(request.UserId);
        }
    };
}