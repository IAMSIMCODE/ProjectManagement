using ProjectManagement.Domain.Models.Http;
using System.Runtime.CompilerServices;

namespace ProjectManagement.Domain.Services.Interfaces
{
    public interface IHttpService
    {
        Task<Response> SendAsync(HttpRequest request, bool enableSSLValidation = true, bool withBearer = true, 
                                [CallerMemberName] string caller = "", string token = "", bool useBasicAuth = false, 
                                string userName = "", string password = "");
    }
}
