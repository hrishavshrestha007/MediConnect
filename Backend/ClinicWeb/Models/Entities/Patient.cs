using System;

namespace ClinicWeb.Models.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        // For ALL users (registered + guests)
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // For registered patients ONLY (nullable for guests)
        public string? PasswordHash { get; set; }  // Only registered users have this
        public string? SocialSecurityNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? TaxNumber { get; set; }
        public string? Religion { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public string? MedicalInsuranceMemberId { get; set; }

        // Helper property to check if registered
        public bool IsRegistered => !string.IsNullOrEmpty(PasswordHash);

        // Navigation
        public List<Appointment>? Appointments { get; set; }
    }
}
