using Microsoft.AspNetCore.Identity;

namespace JoHealth.Models
{
    public class Doctor : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public List<Patient> Patients { get; set; } 
        public List<Appointment> Appointments { get; set; } 
    }
}
