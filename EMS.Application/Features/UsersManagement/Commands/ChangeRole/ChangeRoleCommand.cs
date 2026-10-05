using EMS.Application.Abstractions;

namespace EMS.Application.Features.UsersManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(string UserId, string NewRole) : ICommand { }
}