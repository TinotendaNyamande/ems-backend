using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetUserSummary
{
    public class GetUserSummaryHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<GetUserSummaryQuery, UserTasksSummaryDto>
    {
        public async Task<UserTasksSummaryDto> Handle(GetUserSummaryQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetUserTasksSummaryAsync(request.UserId);
        }
    }
}