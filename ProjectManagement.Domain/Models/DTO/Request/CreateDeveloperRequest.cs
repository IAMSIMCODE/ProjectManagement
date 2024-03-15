namespace ProjectManagement.Domain.Models.DTO.Request
{
    public class CreateDeveloperRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DevNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateDeveloperRequest
    {
        public Guid DeveloperId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DevNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
