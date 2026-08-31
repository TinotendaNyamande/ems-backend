using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(string UserId, string NewRole) : IRequest { }
}