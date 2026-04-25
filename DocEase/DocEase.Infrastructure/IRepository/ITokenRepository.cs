using DocEase.Persistence.Models;
using System.Security.Claims;
namespace DocEase.Infrastructure.IRepository
{
    public interface ITokenRepository
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateAccessToken(string token);
    }
}
