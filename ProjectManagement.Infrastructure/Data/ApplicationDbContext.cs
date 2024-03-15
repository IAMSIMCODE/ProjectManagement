using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : DbContext(option)
    {
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Specify the relationship between the entities
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasOne(d => d.Developer)
                      .WithMany(p => p.Achievements)
                      .HasForeignKey(d => d.DeveloperId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Achivements_Driver");
            });
        }
    }
}
