using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllTasks
{
    public record GetAllTasksQuery():ICommand<IEnumerable<GetTasksDto>> {};
}