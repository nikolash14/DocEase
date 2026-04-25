using DocEase.Application.Dtos.Request;
using DocEase.Application.ServiceReference;
using DocEase.Persistence.Models;
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
            return resp.Status ? Ok(resp) : BadRequest(resp);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromHeader] string token)
        {
            var resp = await _authService.RefreshAsync(token);
            return resp.Status ? Ok(resp) : BadRequest(resp);
        }
    }
}
