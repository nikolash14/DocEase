using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;

namespace DocEase.Application.ServiceReference
{
    public interface IDoctorService
    {
        Task<Response<RegisterDoctorResponse>> RegisterAsync(RegisterDoctorRequest req);
        Task<Response<Boolean>> UpdateRoleAsync(string userName);
        Task<Response<RegisterDoctorResponse>> GetDeatilsAsync(string userName);

    }
}
