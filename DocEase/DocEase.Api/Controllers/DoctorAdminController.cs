using DocEase.Application;
using DocEase.Application.Enums;
using DocEase.Application.ServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocEase.Api.Controllers
{
    [ApiController]
    [Route("doctorAd")]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)}")]
    public class DoctorAdminController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorAdminController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        [Route("updateRole")]
        public async Task<ActionResult> CreateDoctor([FromHeader] string userName)
        {
            var result = await _doctorService.UpdateRoleAsync(userName);
            return result.Status ? NoContent() : BadRequest(result);
        }
    }
}
