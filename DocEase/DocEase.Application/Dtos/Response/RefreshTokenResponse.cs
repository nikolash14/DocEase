namespace DocEase.Application.Dtos.Response
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public Int64 UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}
