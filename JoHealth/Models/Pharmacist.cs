using JoHealth.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Pharmacist : IdentityUser
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

    public ICollection<NewRecord> Records { get; set; } = new List<NewRecord>();

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
