using JoHealth.Models;

public class Doctor : User
{
    public List<Patient> Patients { get; set; } = new List<Patient>();

    public void ViewPatientRecords(Patient patient)
    {
        // Logic to fetch and view patient's records
        var records = patient.MedicalRecords;
    }
}
