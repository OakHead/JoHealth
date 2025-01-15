using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace JoHealth.Models;

public class Patient : IdentityUser
{
    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImageUrl { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public List<Record> MedicalRecords { get; set; } = new List<Record>();
    public Payment PaymentMethod { get; set; }
    public string BloodType { get; set; }
    public string Calories { get; set; }
    public string Weight { get; set; }

    public Appointment BookAppointment(Appointment appointment)
    {
        Appointments.Add(appointment);
        return appointment;
    }

    public void SubmitMedicalRecord(Record record)
    {
        MedicalRecords.Add(record);
    }

    public void UpdatePaymentMethod(Payment payment)
    {
        PaymentMethod = payment;
    }
}
