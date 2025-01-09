using System.ComponentModel.DataAnnotations;

namespace JoHealth.Models;

public class Patient : User
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ImageUrl { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public List<Record> MedicalRecords { get; set; } = new List<Record>();
    public Payment PaymentMethod { get; set; }
    public string BloodType { get; set; }
    public string Calories { get; set; }
    public string Weight { get; set; }
    public int Role { get; set; } // 1 for Patient

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
