using DocEase.Application.Config;
using DocEase.Application.Dtos.Dto;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.Enums;
using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
using DocEase.Persistence.Models;
using Microsoft.Extensions.Options;
namespace DocEase.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly ITokenRepository _tokens;
        private readonly JwtSetting _settings;

        // Simulated stores (swap for DbContext)
        private static readonly List<User> _users = [];
        private static readonly List<RefreshToken> _refreshTokens = [];

        public AuthService(
            ITokenRepository tokens,
            IOptions<JwtSetting> opt)
        {
            _tokens = tokens;
            _settings = opt.Value;

            // Seed a demo admin account on first load
            if (_users.Count == 0)
                _users.Add(new User
                {
                    UserName = "string",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = nameof(UserRoles.Admin),
                });
        }
        public Task<(bool, string?, AuthResponse?)> RegisterAsync(RegisterUserRequest req)
        {
            if (_users.Any(u => u.Email.Equals(req.Email, StringComparison.OrdinalIgnoreCase)))
                return Task.FromResult<(bool, string?, AuthResponse?)>((false, "Email already registered.", null));

            var user = new User
            {
                UserName = req.UserName,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = nameof(UserRoles.Anonymous),
            };

            _users.Add(user);
            return Task.FromResult<(bool, string?, AuthResponse?)>((true, null, BuildResponse(user)));
        }

        // ── Login ─────────────────────────────────────────────────────────────────

        public Task<(bool, string?, AuthResponse?)> LoginAsync(AuthRequest req)
        {
            var user = _users.FirstOrDefault(u =>
                u.UserName.Equals(req.Username, StringComparison.OrdinalIgnoreCase));

            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Task.FromResult<(bool, string?, AuthResponse?)>((false, "Invalid credentials.", null));

            return Task.FromResult<(bool, string?, AuthResponse?)>((true, null, BuildResponse(user)));
        }
        public Task<(bool, string?, AuthResponse?)> RefreshAsync(string refreshToken)
        {
            var stored = _refreshTokens.FirstOrDefault(r =>
                r.Token == refreshToken && !r.IsRevoked && r.ExpiresAt > DateTime.UtcNow);

            if (stored is null)
                return Task.FromResult<(bool, string?, AuthResponse?)>((false, "Invalid or expired refresh token.", null));

            // Rotate: revoke old, issue new
            stored.IsRevoked = true;

            var user = _users.FirstOrDefault(u => u.UserId == stored.UserId);
            if (user is null)
                return Task.FromResult<(bool, string?, AuthResponse?)>((false, "User not found.", null));

            return Task.FromResult<(bool, string?, AuthResponse?)>((true, null, BuildResponse(user)));
        }

        // ── Revoke ────────────────────────────────────────────────────────────────

        public Task<bool> RevokeAsync(string refreshToken)
        {
            var stored = _refreshTokens.FirstOrDefault(r => r.Token == refreshToken);
            if (stored is null) return Task.FromResult(false);
            stored.IsRevoked = true;
            return Task.FromResult(true);
        }

        // ── Helpers ───────────────────────────────────────────────────────────────

        private AuthResponse BuildResponse(User user)
        {
            var accessToken = _tokens.GenerateAccessToken(user);
            var refreshToken = _tokens.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes);

            _refreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenDays),
            });

            return new AuthResponse(
                accessToken, refreshToken, expiresAt,
                new UserDto(user.UserId, user.UserName, user.Email, user.Role));
        }
    }
}
