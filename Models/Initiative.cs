using System.ComponentModel.DataAnnotations;

namespace VolunteerInitiativesSystem.Models
{
    public class Initiative
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

        // Връзка към координатор
        public int CoordinatorId { get; set; }
        public Coordinator Coordinator { get; set; }

        // Много към много
        public ICollection<InitiativeParticipant> InitiativeParticipants { get; set; }
    }
}