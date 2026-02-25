using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolunteerInitiativesSystem.Models
{
    public class Initiative
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;   // 🔥 ДОБАВИХМЕ

        [Required]
        public DateTime Date { get; set; }

        public int MaxParticipants { get; set; }

        // Foreign key
        public int CoordinatorId { get; set; }
        public Coordinator Coordinator { get; set; } = null!;

        // MANY-TO-MANY
        public ICollection<InitiativeParticipant> Registrations { get; set; } 
            = new List<InitiativeParticipant>();
    }
}