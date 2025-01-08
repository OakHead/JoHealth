using JoHealth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JoHealth.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users {  get; set; }

        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);

             // Configure relationships and table mappings

             // Doctor -> Patients relationship
             modelBuilder.Entity<Doctor>()
                 .HasMany(d => d.Patients)
                 .WithMany(p => p.Doctors) // Assuming a many-to-many relationship between Doctors and Patients
                 .UsingEntity(j => j.ToTable("DoctorPatients")); // Join table

             // Doctor -> Appointments relationship
             modelBuilder.Entity<Doctor>()
                 .HasMany(d => d.Appointments)
                 .WithOne(a => a.Doctor)
                 .HasForeignKey(a => a.DoctorId);

             // Patient -> Appointments relationship
             modelBuilder.Entity<Patient>()
                 .HasMany(p => p.Appointments)
                 .WithOne(a => a.Patient)
                 .HasForeignKey(a => a.PatientId);

             // Patient -> MedicalRecords relationship
             modelBuilder.Entity<Patient>()
                 .HasMany(p => p.Records)
                 .WithOne(r => r.Patient)
                 .HasForeignKey(r => r.PatientId);
         }*/
    }
}
