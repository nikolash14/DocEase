using DocEase.Application;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Enums;
using DocEase.Application.ServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{

    [ApiController]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Doctor)}")]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateDoctor([FromBody] RegisterDoctorRequest registerDoctor)

        {
            return Ok(Response<string>.Success("Test"));
        }

        public async Task<IActionResult> GetDetails([FromHeader] string username)
        {
            var result = await _doctorService.GetDeatilsAsync(username);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
