using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailConfigs
{
    public class EditEmailConfigDto
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public EmailType EmailType { get; set; }
        public string? Password { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
    }
}