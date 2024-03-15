using ProjectManagement.Domain.IRepository;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private IDeveloperRepository _developerRepository;
        private IAchievementRepository _achievementRepository;
        private readonly ApplicationDbContext _context;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDeveloperRepository DeveloperRepository
        {
            get
            {
                _developerRepository ??= new DeveloperRepository(_context);
                return _developerRepository;
            }
        }

        public IAchievementRepository AchievementRepository
        {
            get
            {
                _achievementRepository ??= new AchievementRepository(_context);
                return _achievementRepository;
            }
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
