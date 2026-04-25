using DocEase.Api.Common;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{

    [ApiController]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Doctor)}")]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        public DoctorController() { }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateDoctor([FromBody] RegisterDoctorRequest registerDoctor)
        {
            var claimsInfo = User.Claims
        .Select(c => new { c.Type, c.Value })
        .ToList();

            return Ok(ApiResponse<string>.Success("Doctor Created!"));
        }
    }
}
