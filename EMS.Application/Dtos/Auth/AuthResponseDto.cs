using EMS.Application.Dtos.RolesAndPermissions;

namespace EMS.Application.Dtos.Auth
{
    public class AuthResponseDto(string token, string email, string userId, string refreshToken,string? role)
    {

        public string Token { get; set; } = token;
        public string RefreshToken { get; set; } = refreshToken;
        public string Email { get; set; } = email;
        public string UserId { get; set; } = userId;
        public string? Role { get; set; } = role;
        public IEnumerable<GetPermissionDto>? Permissions {  get; set; }
    }
}

