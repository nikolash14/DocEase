using DocEase.Application.Dtos.Request;
using DocEase.Application.Enums;
using DocEase.Application.ServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)}")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var result = await _userService.RegisterAsync(request);
            return result.Status ? Created("201", result) : BadRequest(result);
        }
    }
}
