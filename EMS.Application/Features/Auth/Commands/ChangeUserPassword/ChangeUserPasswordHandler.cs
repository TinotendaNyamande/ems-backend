using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Auth.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordHandler(IAuthService authService,IMapper mapper) : ICommandHandler<ChangeUserPasswordCommand>
    {
        public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = mapper.Map<ChangeUserPasswordDto>(request);
            await authService.ChangePasswordAsync(request.UserId, changePasswordDto);
        }
    }
}
