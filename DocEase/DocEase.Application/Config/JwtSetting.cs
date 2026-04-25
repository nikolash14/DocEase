namespace DocEase.Application.Config
{
    public class JwtSetting
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenMinutes { get; set; } = 20;
        public int RefreshTokenDays { get; set; } = 2;
    }
}
