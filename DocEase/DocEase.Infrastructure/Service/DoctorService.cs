using DocEase.Application;
using DocEase.Application.Dtos.Request;
using DocEase.Application.Dtos.Response;
using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
using Mapster;
namespace DocEase.Infrastructure.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<Response<RegisterDoctorResponse>> GetDeatilsAsync(string userName)
        {
            var result = await _doctorRepository.GetDeatilsAsync(userName);
            if (result is null)
                return Response<RegisterDoctorResponse>.Error("No Records Found!");
            return Response<RegisterDoctorResponse>.Success(result.Adapt<RegisterDoctorResponse>());
        }
        public async Task<Response<bool>> UpdateRoleAsync(string userName)
        {
            var result = await _doctorRepository.UpdateRoleAsync(userName);
            return Response<Boolean>.Success(true);
        }

        public Task<Response<RegisterDoctorResponse>> RegisterAsync(RegisterDoctorRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
