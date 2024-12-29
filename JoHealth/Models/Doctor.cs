namespace JoHealth.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Relationships
        public List<Patient> Patients { get; set; } // List of patients associated with the doctor
        public List<Appointment> Appointments { get; set; } // List of appointments for the doctor
    }
}
