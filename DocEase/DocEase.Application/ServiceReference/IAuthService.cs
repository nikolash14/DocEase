using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;

namespace DocEase.Application.ServiceReference
{
    public interface IAuthService
    {
        Task<(bool Success, string? Error, AuthResponse? Response)> RegisterAsync(RegisterUserRequest req);
        Task<(bool Success, string? Error, AuthResponse? Response)> LoginAsync(AuthRequest req);
        Task<(bool Success, string? Error, AuthResponse? Response)> RefreshAsync(string refreshToken);
        Task<bool> RevokeAsync(string refreshToken);
    }
}
