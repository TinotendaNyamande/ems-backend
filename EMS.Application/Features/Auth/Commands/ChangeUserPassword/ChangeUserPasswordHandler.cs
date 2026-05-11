using AutoMapper;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordHandler(IAuthService authService,IMapper mapper) : IRequestHandler<ChangeUserPasswordCommand>
    {
        public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = mapper.Map<ChangeUserPasswordDto>(request);
            await authService.ChangePasswordAsync(request.UserId, changePasswordDto);
        }
    }
}
