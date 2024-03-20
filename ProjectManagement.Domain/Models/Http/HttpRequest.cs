using static ProjectManagement.Domain.Models.Enums;

namespace ProjectManagement.Domain.Models.Http
{
    public class HttpRequest
    {
        public ApiMethod ApiMethod { get; set; } = ApiMethod.GET;
        public string ApiUrl { get; set; }
        public object Data { get; set; }
    }
}
