namespace DocEase.Application.Dtos.Response
{
    public record RegisterUserResponse
    {
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public DateTime? CreatedAt { get; init; }
        public required string Role { get; init; }
    }
}
