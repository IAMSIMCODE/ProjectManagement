namespace ProjectManagement.Domain.Models
{
    public class Achievement : BaseEntity
    {
        public int OngoingProject { get; set; }
        public int CompletionLevel { get; set; }
        public int CompletedProject { get; set; }
        public int ProjectOnHold { get; set; }
        public int DeployedToProduction { get; set; }
        public Guid DeveloperId { get; set; }

        public virtual Developer Developer { get; set; }
    }
}
