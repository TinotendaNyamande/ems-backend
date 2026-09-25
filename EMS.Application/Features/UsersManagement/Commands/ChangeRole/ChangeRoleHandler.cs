using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.ChangeRole
{
    public class ChangeRoleHandler(
        IAuthService authService
        ) : ICommandHandler<ChangeRoleCommand>
    {
        public async Task Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.NewRole != "Admin" && request.NewRole != "Supervisor" && request.NewRole != "Member")
            {
                throw new ArgumentException("Invalid role specified. Role must be either 'Admin', 'Supervisor', or 'Member'.");
            }

            await authService.ChangeUserRoleAsync(request.UserId, request.NewRole);
        }
    }
}