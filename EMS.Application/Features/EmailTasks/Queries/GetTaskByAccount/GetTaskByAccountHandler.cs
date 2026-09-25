using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAccount
{
    public class GetTaskByAccountHandler(IEmailTasksRepository tasksRepository) : ICommandHandler<GetTaskByAccountQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetTaskByAccountQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTasksForAccountAsync(request.EmailAccountId, request.Status);
        }
    }
}
