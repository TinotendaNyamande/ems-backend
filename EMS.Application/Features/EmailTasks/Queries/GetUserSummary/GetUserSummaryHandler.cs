using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetUserSummary
{
    public class GetUserSummaryHandler(IEmailTasksRepository tasksRepository) : ICommandHandler<GetUserSummaryQuery, UserTasksSummaryDto>
    {
        public async Task<UserTasksSummaryDto> Handle(GetUserSummaryQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetUserTasksSummaryAsync(request.UserId);
        }
    }
}