namespace EMS.Application.Dtos.Auth
{
    public class ChangeUserPasswordDto:LoginUserDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
