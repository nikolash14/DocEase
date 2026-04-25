using DocEase.Application.Dtos.Dto;

namespace DocEase.Application.Dtos.Response
{
    public record AuthResponse(
        string Token, 
        string RefreshToken, 
        DateTime ExpiresAt, 
        UserDto User);
}
