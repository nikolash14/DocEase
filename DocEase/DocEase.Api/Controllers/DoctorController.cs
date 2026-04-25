using DocEase.Application;
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
            return Ok(Response<string>.Success("Test"));
        }
    }
}
