namespace ProjectManagement.Domain.Models.DTO.Response
{
    public class DeveloperAchievementResponse
    {
        public int OngoingProject { get; set; }
        public int CompletionLevel { get; set; }
        public int CompletedProject { get; set; }
        public int ProjectOnHold { get; set; }
        public int DeployedToProduction { get; set; }
        public Guid DeveloperId { get; set; }
    }
}
