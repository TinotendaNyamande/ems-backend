namespace EMS.Application.Dtos.Auth
{
    public record ChangeUserPasswordDto(string Email, string Password, string UserId, string NewPassword);
}