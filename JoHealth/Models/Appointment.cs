namespace JoHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pending";
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        public void ConfirmAppointment()
        {
            Status = "Confirmed";
        }

        public void CancelAppointment()
        {
            Status = "Cancelled";
        }
    }
}
