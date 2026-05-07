using ClinicWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicWeb.Data
{
    public class ClinicDbContext : DbContext
    {

        // DbSets (your tables)
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentCategory> AppointmentCategories { get; set; }

        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // seeding data for Specialities
            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Name = "General Practice" },
                new Speciality { Id = 2, Name = "Cardiology" },
                new Speciality { Id = 3, Name = "Dermatology" },
                new Speciality { Id = 4, Name = "Orthopedics" },
                new Speciality { Id = 5, Name = "Pediatrics" },
                new Speciality { Id = 6, Name = "Neurology" },
                new Speciality { Id = 7, Name = "Psychiatry" }
            );

            // seeding data for Clinics
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic { Id = 1, Name = "MediConnect Central Clinic", Address = "12 Health Ave, Oslo", PhoneNumber = "+47 22 33 44 55" },
                new Clinic { Id = 2, Name = "MediConnect North Branch", Address = "89 Nordic St, Bergen", PhoneNumber = "+47 55 66 77 88" },
                new Clinic { Id = 3, Name = "MediConnect South Branch", Address = "34 Fjord Rd, Stavanger", PhoneNumber = "+47 51 22 33 44" }
            );

            // seeding data for Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "Anna", LastName = "Larsen", ClinicId = 1, SpecialityId = 1 },
                new Doctor { Id = 2, FirstName = "Erik", LastName = "Hansen", ClinicId = 1, SpecialityId = 2 },
                new Doctor { Id = 3, FirstName = "Sofia", LastName = "Berg", ClinicId = 1, SpecialityId = 3 },
                new Doctor { Id = 4, FirstName = "Jonas", LastName = "Nilsen", ClinicId = 2, SpecialityId = 4 },
                new Doctor { Id = 5, FirstName = "Maria", LastName = "Andersen", ClinicId = 2, SpecialityId = 5 },
                new Doctor { Id = 6, FirstName = "Tobias", LastName = "Dahl", ClinicId = 2, SpecialityId = 6 },
                new Doctor { Id = 7, FirstName = "Ingrid", LastName = "Svensson", ClinicId = 3, SpecialityId = 7 },
                new Doctor { Id = 8, FirstName = "Marcus", LastName = "Johansen", ClinicId = 3, SpecialityId = 2 },
                new Doctor { Id = 9, FirstName = "Astrid", LastName = "Olsen", ClinicId = 3, SpecialityId = 1 }
            );

            // seeding data for AppointmentCategories
            modelBuilder.Entity<AppointmentCategory>().HasData(
                new AppointmentCategory { Id = 1, Name = "Consultation" },
                new AppointmentCategory { Id = 2, Name = "Follow-up" },
                new AppointmentCategory { Id = 3, Name = "Emergency" },
                new AppointmentCategory { Id = 4, Name = "Routine Check-up" }
            );

            //Coverting appointment status to string for better readability in the database
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();
        }
    }
}
