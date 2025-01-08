﻿namespace JoHealth.Models;

public class Doctor : User
{
    public string Specialty { get; set; }
    public string ImageUrl { get; set; }
    public List<Patient> Patients { get; set; } = new List<Patient>();

    public void ViewPatientRecords(Patient patient)
    {
        var records = patient.MedicalRecords;
    }
}
