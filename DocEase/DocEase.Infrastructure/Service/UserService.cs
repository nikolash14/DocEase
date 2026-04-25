using DocEase.Application;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.Enums;
using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
using DocEase.Infrastructure.Utility;
using DocEase.Persistence.Models;
using Mapster;
namespace DocEase.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<RegisterUserResponse>>
            RegisterAsync(RegisterUserRequest req)
        {
            var user = new User
            {
                UserName = req.UserName,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = nameof(UserRoles.Patient),
            };
            var result = await _userRepository.ResisterUser(user);
            if (result is null)
                return Response<RegisterUserResponse>.Error(
                    string.Format(Message.UserCouldNotCreated, req.UserName));
            return Response<RegisterUserResponse>.Success(result.Adapt<RegisterUserResponse>());
        }
    }
}