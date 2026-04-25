using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;

namespace DocEase.Application.ServiceReference
{
    public interface IAuthService
    {
        Task<Response<AuthResponse>> LoginAsync(AuthRequest req);
        Task<Response<AuthResponse>> RefreshAsync(string refreshToken);
        Task<Response<bool>> RevokeAsync(string refreshToken);
    }
}
