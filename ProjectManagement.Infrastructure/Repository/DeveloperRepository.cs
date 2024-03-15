using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repository
{
    public class DeveloperRepository(ApplicationDbContext context) : GenericRepository<Developer>(context), IDeveloperRepository
    {
        public override async Task<bool> UpdateAsync(Developer entity)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null) { return false; }

            result.UpdatedDate = DateTime.Now;
            result.DevNumber = entity.DevNumber;
            result.FirstName = entity.FirstName;
            result.LastName = entity.LastName;
            result.DateOfBirth = entity.DateOfBirth;

            return true;
        }
    }
}
