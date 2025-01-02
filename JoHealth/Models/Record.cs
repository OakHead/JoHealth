using System.Collections.Generic;

namespace JoHealth.Models
{
    public class Record
    {
        public int Id { get; set; } // Unique identifier for the record
        public string PatientId { get; set; } // ID of the patient
        public string DoctorId { get; set; } // ID of the doctor (assigned after the appointment)
        public string Description { get; set; } // Description or symptoms submitted by the patient
        public string Notes { get; set; } // Doctor's notes after the appointment
        public List<Medicine> RecommendedMedications { get; set; } = new List<Medicine>(); // Recommended medications
        public DateTime SubmissionDate { get; set; } // Date when the record was submitted
        public DateTime? UpdatedDate { get; set; } // Date when the doctor updated the record

        // Method to add doctor's notes and medications
        public void UpdateRecord(string doctorId, string notes, List<Medicine> medications)
        {
            DoctorId = doctorId;
            Notes = notes;
            RecommendedMedications = medications;
            UpdatedDate = DateTime.Now;
        }
    }
}
