using JoHealth.Models;

public class Patient : User
{
    public string ImageUrl { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public List<Record> MedicalRecords { get; set; } = new List<Record>();
    public Payment PaymentMethod { get; set; }

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
