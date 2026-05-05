namespace EMS.Infrastructure.persistence
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Token { get; set; }
        public string TokenHash { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
    }
}
