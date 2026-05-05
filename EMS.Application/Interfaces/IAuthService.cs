using EMS.Application.Dtos.Auth;

namespace EMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterUserDto request);
        Task<AuthResponseDto> LoginAsync(LoginUserDto request);
        Task LogoutAsync(string userId);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

    }
}
