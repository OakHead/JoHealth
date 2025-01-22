using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace JoHealth.Models;

public class Doctor : IdentityUser
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string Specialty { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    // Many-to-Many Relationship with NewRecord
    public ICollection<NewRecord> Records { get; set; } = new List<NewRecord>();
}
