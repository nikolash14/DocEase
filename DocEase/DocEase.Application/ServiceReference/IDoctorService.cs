using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;

namespace DocEase.Application.ServiceReference
{
    public interface IDoctorService
    {
        Task<(bool Success, string? Error, RegisterDoctorResponse? Response)> RegisterAsync(RegisterDoctorRequest req);
    }
}
