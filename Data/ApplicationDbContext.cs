using Microsoft.EntityFrameworkCore;
using VolunteerInitiativesSystem.Models;

namespace VolunteerInitiativesSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Initiative> Initiatives { get; set; }
        public DbSet<InitiativeParticipant> InitiativeParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InitiativeParticipant>()
                .HasKey(ip => new { ip.InitiativeId, ip.ParticipantId });

            base.OnModelCreating(modelBuilder);
        }
    }
}