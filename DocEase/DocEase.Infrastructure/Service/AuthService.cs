using DocEase.Application;
using DocEase.Application.Config;
using DocEase.Application.Dtos.Dto;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
using DocEase.Infrastructure.Utility;
using DocEase.Persistence.Models;
using Mapster;
using Microsoft.Extensions.Options;
namespace DocEase.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly ITokenRepository _tokens;
        private readonly JwtSetting _settings;
        private readonly IUserRepository _userRepository;
        public AuthService(
            ITokenRepository tokens,
            IOptions<JwtSetting> opt, IUserRepository userRepository)
        {
            _tokens = tokens;
            _settings = opt.Value;
            _userRepository = userRepository;
        }
        public async Task<Response<AuthResponse>> LoginAsync(AuthRequest req)
        {
            var user = await _userRepository.GetUserAsync(req.Username);
            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Response<AuthResponse>.Error(string.Format(Message.InvalidUserNameOrPassword, req.Username));
            return Response<AuthResponse>.Success(await CreateAccessToken(user));
        }

        private async Task<AuthResponse> CreateAccessToken(User user, bool isRefresh = false)
        {
            var accessToken = _tokens.GenerateAccessToken(user);
            var refreshToken = _tokens.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes);
            if (isRefresh)
            {
                await _userRepository.UpdateRefreshToken(new RefreshToken
                {
                    TokenHash = refreshToken,
                    UserId = user.UserId,
                    ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenDays),
                });
            }
            else
            {
                await _userRepository.RegisterRefreshToken(new RefreshToken
                {
                    TokenHash = refreshToken,
                    UserId = user.UserId,
                    ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenDays),
                });
            }
            var response = new AuthResponse(
                accessToken, refreshToken, expiresAt,
                user.Adapt<UserDto>());
            return response;
        }

        public async Task<Response<AuthResponse>> RefreshAsync(string refreshToken)
        {
            var stored = await _userRepository.GetTokenRefreshAsync(refreshToken);
            if (stored is null)
                return Response<AuthResponse>.Error(Message.InvalidUserNameOrPassword);

            // Rotate: revoke old, issue new
            await RevokeAsync(refreshToken);

            var user = await _userRepository.GetUserAsync(stored.UserId);
            if (user is null)
                return Response<AuthResponse>.Error(Message.UserNotAvailable);
            return Response<AuthResponse>.Success(await CreateAccessToken(user, true));
        }
        public async Task<Response<bool>> RevokeAsync(string refreshToken)
        {
            var stored = await _userRepository.RevokeTokenAsync(refreshToken);
            if (stored > 0)
                return Response<bool>.Success(true);
            return Response<bool>.Success(false);
        }
    }
}
