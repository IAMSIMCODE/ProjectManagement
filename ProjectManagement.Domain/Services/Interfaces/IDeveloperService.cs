using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Models.DTO.Response;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManagement.Domain.Services.Interfaces
{
    public interface IDeveloperService
    {
        Task<List<GetDeveloperResponse>> GetAll();
        Task<GetDeveloperResponse> GetById(Guid developerId);
        Task<GetDeveloperResponse> InsertAsync([NotNull] CreateDeveloperRequest request);
        Task<bool> UpdateAsync([NotNull]UpdateDeveloperRequest updateDeveloper);
        Task<bool> DeleteAsync(Guid developerId);
    }
}
