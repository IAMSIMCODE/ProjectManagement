using AutoMapper;
using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Models.DTO.Response;
using ProjectManagement.Domain.Services.Interfaces;
using ProjectManagement.Domain.Utility;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManagement.Domain.Services
{
    public class AchievementService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper) : IAchievementService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly ILoggerManager _loggerManager = loggerManager;
        private readonly IMapper _mapper = mapper;

        public async Task<List<DeveloperAchievementResponse>> GetAll()
        {
            var achievements = await _repositoryManager.AchievementRepository.GetAllAsync(null, null, "", false);
            if (achievements == null) { return null; }

            var result = _mapper.Map<List<DeveloperAchievementResponse>>(achievements);
            return result;
        }

        public async Task<DeveloperAchievementResponse> GetById(Guid developerId)
        {
            var achievement = await _repositoryManager.AchievementRepository.GetSingleByCondition(x => x.Id == developerId);
            if (achievement == null) { return null; }

            var response = _mapper.Map<DeveloperAchievementResponse>(achievement);
            return response;
        }

        public async Task<DeveloperAchievementResponse> InsertAsync([NotNull] CreateDevAchievementRequest request)
        {
            var achievement = _mapper.Map<Achievement>(request);
            var result = await _repositoryManager.AchievementRepository.InsertAsync(achievement);

            await _repositoryManager.SaveChangesAsync();
            if (result == null) { return null; }    

            var response = _mapper.Map<DeveloperAchievementResponse>(result);
            return response;
        }

        public async Task<bool> UpdateAsync([NotNull] UpdateDevAchievementRequest update)
        {
            var achievement = await _repositoryManager.AchievementRepository.GetSingleByCondition(x => x.Id == update.DeveloperId);

            achievement.UpdatedDate = DateTime.Now;
            achievement.OngoingProject = update.OngoingProject;
            achievement.CompletionLevel = update.CompletionLevel;
            achievement.CompletedProject = update.CompletedProject;
            achievement.ProjectOnHold = update.ProjectOnHold;
            achievement.DeployedToProduction = update.DeployedToProduction;

            var result = await _repositoryManager.AchievementRepository.UpdateAsync(achievement);
            await _repositoryManager.SaveChangesAsync();

            if (!result) {  return false; }
            return result;
        }

        public async Task<bool> DeleteAsync(Guid developerId)
        {
            var achievement = await _repositoryManager.AchievementRepository.GetSingleByCondition(x => x.Id == developerId);

            if (achievement == null) { return false; }

            var result = await _repositoryManager.AchievementRepository.DeleteAsync(achievement);
            await _repositoryManager.SaveChangesAsync();

            if (!result) { return false; }
            return result;
        }
    }
}
