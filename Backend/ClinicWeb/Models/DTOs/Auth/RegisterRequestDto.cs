using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicWeb.Models.DTOs
{
    public class RegisterRequestDto
{

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    // Sensitive PII - only for registered users
    public string? SocialSecurityNumber { get; set; }
    
    public DateTime? Birthdate { get; set; }
    
    [StringLength(20)]
    public string? Gender { get; set; }
    
    public string? TaxNumber { get; set; }
    
    [StringLength(50)]
    public string? Religion { get; set; }
    
    public string? DriverLicenseNumber { get; set; }
    
    public string? MedicalInsuranceMemberId { get; set; }
}
}
