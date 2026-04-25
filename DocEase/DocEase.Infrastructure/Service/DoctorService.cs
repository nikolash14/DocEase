using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
namespace DocEase.Infrastructure.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<(
            bool Success,
            string? Error,
            RegisterDoctorResponse? Response)>
            RegisterAsync(RegisterDoctorRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
