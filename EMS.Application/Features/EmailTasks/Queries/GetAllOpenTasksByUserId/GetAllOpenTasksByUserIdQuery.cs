using EMS.Application.Abstractions;
using EMS.Application.Dtos.Tasks;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId
{
    public record GetAllOpenTasksByUserIdQuery(string UserId):ICommand<IEnumerable<GetTasksDto>> {};
}