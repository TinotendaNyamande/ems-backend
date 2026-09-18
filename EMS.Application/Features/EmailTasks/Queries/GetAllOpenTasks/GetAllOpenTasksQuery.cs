using EMS.Application.Dtos.Tasks;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasks
{
    public record GetAllOpenTasksQuery() :IRequest<IEnumerable<GetTasksDto>> {};
}