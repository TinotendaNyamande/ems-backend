using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.AssignUserRole
{
    public record AssignUserRoleCommand(string UserId, Guid RoleId) : IRequest
    {
    }
}
