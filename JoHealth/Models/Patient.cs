using Microsoft.AspNetCore.Identity;

namespace JoHealth.Models
{
    public class Patient : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
