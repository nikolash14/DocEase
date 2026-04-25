using DocEase.Infrastructure.IRepository;
using DocEase.Persistence.Data;
using DocEase.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DocEase.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private DocEaseDbContext _docEaseDbContext;
        public UserRepository(DocEaseDbContext docEaseDbContext)
        {
            _docEaseDbContext = docEaseDbContext;
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _docEaseDbContext
                    .Users
                    .FirstOrDefaultAsync(m => m.UserName
                                .Equals(username) && m.IsActive == true);
        }

        public async Task<User?> GetUserAsync(Int64 id)
        {
            return await _docEaseDbContext
                    .Users
                    .FirstOrDefaultAsync(m => m.UserId == id && m.IsActive == true);
        }

        public async Task RegisterRefreshToken(RefreshToken refreshToken)
        {
            await _docEaseDbContext.RefreshTokens.AddAsync(refreshToken);
            await _docEaseDbContext.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(RefreshToken refreshToken)
        {
            var result = await _docEaseDbContext
                                .RefreshTokens
                                .FirstAsync(m => m.UserId == refreshToken.UserId);
            result.TokenHash = refreshToken.TokenHash;
            result.CreatedAt = refreshToken.CreatedAt;
            result.ExpiresAt = refreshToken.ExpiresAt;
            result.RevokedAt = null;
            _docEaseDbContext.RefreshTokens.Update(refreshToken);
            await _docEaseDbContext.SaveChangesAsync();
        }

        public async Task<User?> ResisterUser(User user)
        {
            _docEaseDbContext.Users.Add(user);
            await _docEaseDbContext.SaveChangesAsync();
            return await _docEaseDbContext
                    .Users
                    .FirstAsync(m => m.UserName == user.UserName);
        }

        public async Task<RefreshToken?> GetTokenRefreshAsync(string refreshToken)
        {
            return await _docEaseDbContext.RefreshTokens.FirstOrDefaultAsync(r =>
                                    r.TokenHash == refreshToken &&
                                    !r.RevokedAt.HasValue &&
                                    r.ExpiresAt > DateTime.UtcNow);
        }

        public async Task<Int64> RevokeTokenAsync(string refreshToken)
        {
            var result = await _docEaseDbContext.RefreshTokens.FirstAsync(m => m.TokenHash == refreshToken);
            result.RevokedAt = DateTime.Now;
            _docEaseDbContext.RefreshTokens.Update(result);
            return await _docEaseDbContext.SaveChangesAsync();
        }

        public async Task<long> DeActiveUser(string userName)
        {
            var result = await _docEaseDbContext.Users.FirstAsync(m => m.UserName.Equals(userName));
            result.IsActive = false;
            _docEaseDbContext.Users.Update(result);
            var dbResult = await _docEaseDbContext.SaveChangesAsync();
            return dbResult;
        }
    }
}
