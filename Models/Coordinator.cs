using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VolunteerInitiativesSystem.Models
{
    public class Coordinator
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Initiative> Initiatives { get; set; }
    }
}