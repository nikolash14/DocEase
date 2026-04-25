using DocEase.Application.Enums;
using DocEase.Infrastructure.IRepository;
using DocEase.Persistence.Data;
using DocEase.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DocEase.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private DocEaseDbContext _docEaseDbContext;
        public DoctorRepository(DocEaseDbContext docEaseDbContext)
        {
            _docEaseDbContext = docEaseDbContext;
        }
        public async Task<Doctor?> GetDeatilsAsync(string userName)
        {
            var query = from user in _docEaseDbContext.Users
                        join doctor in _docEaseDbContext.Doctors on user.UserId equals doctor.UserId
                        where user.UserName == userName && user.IsActive == true
                        select doctor;
            return await query.FirstAsync();
        }

        public async Task<long> UpdateRoleAsync(string userName)
        {
            var result = await _docEaseDbContext.Users.FirstAsync(m => m.UserName == userName);
            result.Role = nameof(UserRoles.Doctor);
            _docEaseDbContext.Update(result);
            return await _docEaseDbContext.SaveChangesAsync();
        }
    }
}
