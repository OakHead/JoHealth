using JoHealth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<NewRecord> NewRecords { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many between Doctor and Patient
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Patients)
                .WithMany(p => p.Doctors)
                .UsingEntity(j => j.ToTable("DoctorPatients"));

            // Configure many-to-many between Doctor and NewRecord
            modelBuilder.Entity<NewRecord>()
                .HasMany(r => r.Doctors)
                .WithMany(d => d.Records)
                .UsingEntity(j => j.ToTable("NewRecordDoctors"));

            // Configure many-to-many between Patient and NewRecord
            modelBuilder.Entity<NewRecord>()
                .HasMany(r => r.Patients)
                .WithMany(p => p.Records)
                .UsingEntity(j => j.ToTable("NewRecordPatients"));

            // Configure many-to-many between Pharmacist and Doctor
            //modelBuilder.Entity<Pharmacist>()
            //    .HasMany(ph => ph.Doctors)
            //    .WithMany(d => d.Pharmacists)
            //    .UsingEntity(j => j.ToTable("PharmacistDoctors"));

            // Configure many-to-many between Pharmacist and NewRecord
            modelBuilder.Entity<NewRecord>()
                .HasMany(r => r.Pharmacists)
                .WithMany(ph => ph.Records)
                .UsingEntity(j => j.ToTable("NewRecordPharmacists"));
        }
    }
}
