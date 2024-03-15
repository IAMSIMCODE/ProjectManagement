namespace ProjectManagement.Domain.Models
{
    public class Developer : BaseEntity
    {
        public Developer()
        {
            //Achievements = new HashSet<Achievement>(); You can either declear it like this or just simplify it
            Achievements = [];
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DevNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
