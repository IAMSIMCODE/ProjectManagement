using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Models.DTO.Response;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManagement.Domain.Services.Interfaces
{
    public interface IAchievementService
    {
        Task<List<DeveloperAchievementResponse>> GetAll();
        Task<DeveloperAchievementResponse> GetById(Guid developerId);
        Task<DeveloperAchievementResponse> InsertAsync([NotNull] CreateDevAchievementRequest request);
        Task<bool> UpdateAsync([NotNull] UpdateDevAchievementRequest updateDeveloper);
        Task<bool> DeleteAsync(Guid developerId);
    }
}
