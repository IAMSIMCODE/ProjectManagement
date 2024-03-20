using Newtonsoft.Json;
using ProjectManagement.Domain.Models.Http;
using ProjectManagement.Domain.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using static ProjectManagement.Domain.Models.Enums;

namespace ProjectManagement.Domain.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _url = "";

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response> SendAsync(HttpRequest request, bool enableSSLValidation = true, bool withBearer = true, [CallerMemberName] string caller = "", string token = "", bool useBasicAuth = false, string userName = "", string password = "")
        {
            try
            {
                _url = request.ApiUrl;
                var httpClient = _httpClientFactory.CreateClient();

                var message = new HttpRequestMessage
                {
                    RequestUri = new Uri(request.ApiUrl),
                    Method = GetHttpMethod(request.ApiMethod)
                };

                message.Headers.Add("Accept", "application/json");

                string accessToken = "";
                if (withBearer && token.Length > 0) { accessToken = token; }

                if (!string.IsNullOrEmpty(accessToken)) 
                { message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken); }

                if (useBasicAuth)
                {
                    var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
                    message.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);
                }

                if (request.Data != null)
                {
                    var requestString = JsonConvert.SerializeObject(request.Data);
                    message.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                }

                var remoteResponse = await httpClient.SendAsync(message); 
                return await ProcessRemoteResponse(remoteResponse);
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = $"An Error Occured! | URL: {_url} Exception: {ex.Message} : {ex.StackTrace}"};
            }
        }

        private async Task<Response> ProcessRemoteResponse(HttpResponseMessage remoteResponse)
        {
            var responseString = await remoteResponse.Content.ReadAsStringAsync();  
            
            switch (remoteResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    return ReturnNewResponse(true, "Successful", responseString, 200);

                case HttpStatusCode.Unauthorized:
                    return ReturnNewResponse(false, "Unauthorized Request", responseString, 401);

                case HttpStatusCode.NotFound:
                    return ReturnNewResponse(false, "Not Found", responseString, 404);

                case HttpStatusCode.BadRequest:
                    return ReturnNewResponse(false, "Bad Request", responseString, 400);

                case HttpStatusCode.BadGateway:
                    return ReturnNewResponse(false, "Bad Gateway", responseString, 502);

                case HttpStatusCode.GatewayTimeout:
                    return ReturnNewResponse(false, "Gateway Timeout", responseString, 504);

                default:
                    responseString = null;
                    return new Response { IsSuccess = false, Message = $"HttpRequest returned bad response:{responseString}",Result=responseString };
            }
        }

        private static Response ReturnNewResponse(bool isSuccess, string message, string responseString, int statusCode)
        {
            return new Response { IsSuccess = isSuccess, Message = message, Result = responseString, StatusCode = statusCode, };
        }

        private HttpMethod GetHttpMethod(ApiMethod apiMethod)
        {
            return apiMethod switch
            {
                ApiMethod.POST => HttpMethod.Post,
                ApiMethod.PUT => HttpMethod.Put,
                ApiMethod.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
        }
    }
}
