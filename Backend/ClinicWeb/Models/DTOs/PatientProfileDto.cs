using System;

namespace ClinicWeb.Models.DTOs
{
    public class PatientProfileDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string? TaxNumber { get; set; }
        public string? Religion { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public string? MedicalInsuranceMemberId { get; set; }
    }
}
