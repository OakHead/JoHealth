namespace JoHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Unique identifier for the appointment
        public string PatientId { get; set; } // The ID of the patient (could be a Patient or Admin user)
        public string DoctorId { get; set; } // The ID of the doctor
        public DateTime AppointmentDate { get; set; } // Date and time of the appointment
        public string Status { get; set; } = "Pending"; // Status: Pending, Confirmed, Completed, Cancelled

        // Navigation Properties
        public Patient Patient { get; set; } // Reference to the Patient class
        public Doctor Doctor { get; set; } // Reference to the Doctor class

        // Method to confirm the appointment
        public void ConfirmAppointment()
        {
            Status = "Confirmed";
        }

        // Method to cancel the appointment
        public void CancelAppointment()
        {
            Status = "Cancelled";
        }
    }
}
