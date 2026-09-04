using EMS.Application.Dtos.Tasks;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetUserSummary
{
    public record GetUserSummaryQuery(string UserId):IRequest<UserTasksSummaryDto> {};
}