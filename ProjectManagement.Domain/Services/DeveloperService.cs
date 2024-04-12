using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Models.DTO.Response;
using ProjectManagement.Domain.Models.Http;
using ProjectManagement.Domain.Pagination;
using ProjectManagement.Domain.Services.Interfaces;
using ProjectManagement.Domain.Utility;
using System.Diagnostics.CodeAnalysis;
using X.PagedList;
using static ProjectManagement.Domain.Models.Enums;

namespace ProjectManagement.Domain.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IHttpService _httpService;

        public DeveloperService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IHttpService httpService)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _httpService = httpService;
        }

        public async Task<List<GetDeveloperResponse>> GetAll()
        {
            var developers = await _repositoryManager.DeveloperRepository.GetAllAsync(x => x.Status == 1, 
                                                                                      x => x.OrderBy(x => x.AddedDate), "", false);;
            if (developers == null) { return null; }

            var result = _mapper.Map<List<GetDeveloperResponse>>(developers);
            return result;
        }

        public async Task<List<GetDeveloperResponse>> GetPageList(RequestParams requestParams)
        {
            //var dev = await _repositoryManager.DeveloperRepository.GetAll(requestParams);

            List<Developer> developers = new();

            for (int i = 0; i < 60; i++)
            {
                developers.Add(new Developer { FirstName = "Test" });
            }

            var dev = await developers.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
            //var developers = await _repositoryManager.DeveloperRepository.GetAllAsync(x => x.Status == 1,
            //                                                                          x => x.OrderBy(x => x.AddedDate), "", false); ;
            //if (developers == null) { return null; }

            var result = _mapper.Map<List<GetDeveloperResponse>>(dev);
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

        public async Task<List<Rootobject>> GetApiDevelopers()
        {
            var dev = await _httpService.SendAsync(new HttpRequest
            {
                ApiMethod = ApiMethod.GET,
                ApiUrl = "https://localhost:44395/api/v2/Developer",
                Data = new List<Rootobject> {  new Rootobject() { } }
            });

            if (dev == null) { return null; }

            List<Rootobject> rootobjects = new List<Rootobject>();
            
            var rotObj = JsonConvert.DeserializeObject<List<Class1>>(dev.Result.ToString());

            rootobjects.Add(new Rootobject
            {
                Property1 = rotObj.ToArray()
            });

            return rootobjects;
        }
    }
}
