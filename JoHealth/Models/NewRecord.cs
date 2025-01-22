using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JoHealth.Models
{
    public class NewRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Age { get; set; } // Required

        [Required]
        public string Gender { get; set; } // Required

        [Required]
        public string Description { get; set; } // Required

        [Required]
        public string FirstName { get; set; } // Required

        [Required]
        public string LastName { get; set; } // Required

        public string? DoctorId { get; set; }

        public string? PatientId { get; set; }

        public string? PharmacistId { get; set; }

        public string? Prescriptions { get; set; } // New property for prescriptions

        public bool IsApprovedByDoctor { get; set; } // New flag for approval
        public bool IsAdministeredByPharmacist { get; set; } // New flag for pharmacist administration

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // not imp

        // Many-to-Many Relationships
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<Pharmacist> Pharmacists { get; set; } = new List<Pharmacist>();
    }
}
