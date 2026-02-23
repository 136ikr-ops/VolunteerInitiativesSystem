using System.Collections.Generic;

namespace VolunteerInitiativesSystem.Models
{
    public class InitiativeParticipant
    {
        public int InitiativeId { get; set; }
        public Initiative Initiative { get; set; }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}