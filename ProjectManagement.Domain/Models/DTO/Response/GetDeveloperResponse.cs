namespace ProjectManagement.Domain.Models.DTO.Response
{
    public class GetDeveloperResponse
    {
        public Guid DeveloperId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DevNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
