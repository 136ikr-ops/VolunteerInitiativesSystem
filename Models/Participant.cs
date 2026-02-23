using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VolunteerInitiativesSystem.Models
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<InitiativeParticipant> InitiativeParticipants { get; set; }
    }
}