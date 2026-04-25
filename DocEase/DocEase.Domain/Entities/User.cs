namespace DocEase.Domain.Entities
{
    public record User
    {
        public int UserId { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? PasswordHash { get; init; }
        public string? Role { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? LastLogin { get; init; }
    }
}
