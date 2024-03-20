namespace ProjectManagement.Domain.Models.Http
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
    }
}
