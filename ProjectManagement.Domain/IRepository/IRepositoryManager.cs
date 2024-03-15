namespace ProjectManagement.Domain.IRepository
{
    public interface IRepositoryManager
    {
        IDeveloperRepository DeveloperRepository { get; }
        IAchievementRepository AchievementRepository { get; }
        Task SaveChangesAsync();
    }
}
