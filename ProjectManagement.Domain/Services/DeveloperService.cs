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
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public DeveloperService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<List<GetDeveloperResponse>> GetAll()
        {
            var developers = await _repositoryManager.DeveloperRepository.GetAllAsync(x => x.Status == 1, 
                                                                                      x => x.OrderBy(x => x.AddedDate), "", false);;
            if (developers == null) { return null; }

            var result = _mapper.Map<List<GetDeveloperResponse>>(developers);
            return result;
        }

        public async Task<GetDeveloperResponse> GetById(Guid developerId)
        {
            var developer = await _repositoryManager.DeveloperRepository.GetSingleByCondition(x => x.Id == developerId);
            if (developer == null) { return null; }

            var response = _mapper.Map<GetDeveloperResponse>(developer);
            return response;
        }

        public async Task<GetDeveloperResponse> InsertAsync([NotNull] CreateDeveloperRequest request)
        {
            var developer = _mapper.Map<Developer>(request);
            var result = await _repositoryManager.DeveloperRepository.InsertAsync(developer);
            await _repositoryManager.SaveChangesAsync();

            var response = _mapper.Map<GetDeveloperResponse>(result);
            return response;
        }

        public async Task<bool> UpdateAsync([NotNull] UpdateDeveloperRequest updateDeveloper)
        {
            var developer = _mapper.Map<Developer>(updateDeveloper);

            var result = await _repositoryManager.DeveloperRepository.UpdateAsync(developer);
            await _repositoryManager.SaveChangesAsync();

            if (!result) {  return false; }
            return result;
        }

        public async Task<bool> DeleteAsync(Guid developerId)
        {
            var developer = await _repositoryManager.DeveloperRepository.GetSingleByCondition(x => x.Id == developerId);
            if (developer == null) { return false; }

            var result = await _repositoryManager.DeveloperRepository.DeleteAsync(developer);
            await _repositoryManager.SaveChangesAsync();

            if (!result) { return false; }
            return result;
        }
    }
}
