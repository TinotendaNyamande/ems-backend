using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(string UserId, string NewRole) : ICommand { }
}