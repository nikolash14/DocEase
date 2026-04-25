using DocEase.Api.Common;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.ServiceReference;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest user)
        {
            var resp = await _authService.LoginAsync(user);
            if (resp.Success && resp.Response is not null)
                return Ok(ApiResponse<AuthResponse>.Success(resp.Response));
            else
                return BadRequest(ApiResponse<string>.Fail(resp.Error));
        }
    }
}
