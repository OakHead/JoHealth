using System.ComponentModel.DataAnnotations;

namespace JoHealth.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ImageUrl { get; set; } // Optional, so no [Required] here

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Doctor-specific fields
        public string Specialty { get; set; }

        // Pharmacist-specific fields
        public string PharmacyName { get; set; }

        // Admin-specific fields
        public string AdminCode { get; set; }
    }

}
