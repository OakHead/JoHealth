using System.Collections.Generic;

namespace JoHealth.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public List<Medicine> RecommendedMedications { get; set; } = new List<Medicine>();
        public DateTime SubmissionDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public void UpdateRecord(string doctorId, string notes, List<Medicine> medications)
        {
            DoctorId = doctorId;
            Notes = notes;
            RecommendedMedications = medications;
            UpdatedDate = DateTime.Now;
        }
    }
}
