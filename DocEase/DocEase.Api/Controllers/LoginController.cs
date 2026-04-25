using DocEase.Api.Common;
using DocEase.Application.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest user)
        {
            var resp = ApiResponse<string>.Success("Some Message");
            await Task.Delay(2000);
            return Ok(resp);
        }
    }
}
