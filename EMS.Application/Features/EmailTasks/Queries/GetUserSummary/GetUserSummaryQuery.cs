using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Features.EmailTasks.Queries.GetUserSummary
{
    public record GetUserSummaryQuery(string UserId):ICommand<UserTasksSummaryDto> {};
}