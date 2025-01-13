using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JoHealth.Models;

public class Doctor : IdentityUser
{
    public string FirstName { get; set; }
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
    public List<Patient> Patients { get; set; } = new List<Patient>();

    public void ViewPatientRecords(Patient patient)
    {
        var records = patient.MedicalRecords;
    }
}
