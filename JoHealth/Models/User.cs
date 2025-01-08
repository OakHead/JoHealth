using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JoHealth.Models
{
    public abstract class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void EditProfile(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public List<Article> ViewArticles(List<Article> articles)
        {
            return articles; 
        }

        public List<Appointment> ViewAppointments(List<Appointment> appointments)
        {
            return appointments;
        }
    }
}
