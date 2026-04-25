using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;

namespace DocEase.Application.ServiceReference
{
    public interface IUserService
    {
        Task<Response<RegisterUserResponse>> RegisterAsync(RegisterUserRequest req);
        Task<Response<Boolean>> DeActiveUserAsync(string userName);
    }
}
