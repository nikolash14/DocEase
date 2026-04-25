using DocEase.Application.Dtos.Response;
using DocEase.Persistence.Models;

namespace DocEase.Infrastructure.IRepository
{
    public interface IDoctorRepository
    {
        Task<Int64> UpdateRoleAsync(string userName);
        Task<Doctor?> GetDeatilsAsync(string userName);
    }
}
