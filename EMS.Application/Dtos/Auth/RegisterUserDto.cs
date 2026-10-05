namespace EMS.Application.Dtos.Auth
{
    public record RegisterUserDto(string FirstName, string LastName, string Email, string Password,string Role);
    
}
