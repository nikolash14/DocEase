using DocEase.Persistence.Models;

namespace DocEase.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task<User?> ResisterUser(User user);
        Task<User?> GetUserAsync(string username);
        Task<User?> GetUserAsync(Int64 id);
        Task RegisterRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetTokenRefreshAsync(string refreshToken);
        Task<Int64> RevokeTokenAsync(string refreshToken);
        Task UpdateRefreshToken(RefreshToken refreshToken);
    }
}
