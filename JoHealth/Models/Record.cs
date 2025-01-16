using System.Collections.Generic;

namespace JoHealth.Models
{
    public class Record
    {
        public int Id { get; set; } // Primary key for the Record
        public string DoctorId { get; set; } // Maps to IdentityUser's Id
        public Doctor Doctor { get; set; } // Navigation property
        public string PatientId { get; set; } // Foreign key to Patient (IdentityUser Id)
        public Patient Patient { get; set; } // Navigation property
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
