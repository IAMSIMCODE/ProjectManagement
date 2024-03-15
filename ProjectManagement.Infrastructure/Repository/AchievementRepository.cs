using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repository
{
    public class AchievementRepository(ApplicationDbContext context) : GenericRepository<Achievement>(context), IAchievementRepository
    {
       
    }
}
