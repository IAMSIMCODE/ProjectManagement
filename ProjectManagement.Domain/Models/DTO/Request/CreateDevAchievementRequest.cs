namespace ProjectManagement.Domain.Models.DTO.Request
{
    public class CreateDevAchievementRequest
    {
        public Guid DeveloperId { get; set; }
        public int OngoingProject { get; set; }
        public int CompletionLevel { get; set; }
        public int CompletedProject { get; set; }
        public int ProjectOnHold { get; set; }
        public int DeployedToProduction { get; set; }
    }

    public class UpdateDevAchievementRequest
    {
        public Guid DeveloperId { get; set; }
        public int OngoingProject { get; set; }
        public int CompletionLevel { get; set; }
        public int CompletedProject { get; set; }
        public int ProjectOnHold { get; set; }
        public int DeployedToProduction { get; set; }
    }
}
