using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace JoHealth.Models;

public class Patient : IdentityUser
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

    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "Blood Type is required.")]
    [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood Type format.")]
    public string BloodType { get; set; }

    [Required(ErrorMessage = "Weight is required.")]
    public string Weight { get; set; }

    // Many-to-Many Relationship with NewRecord
    public ICollection<NewRecord> Records { get; set; } = new List<NewRecord>();

    // Many-to-Many Relationship with Doctor
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
