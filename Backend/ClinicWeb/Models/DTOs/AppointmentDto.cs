using System;

namespace ClinicWeb.Models.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        public string? Status { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpeciality { get; set; }
        public string? ClinicName { get; set; }
        public string? CategoryName { get; set; }
        public string? PatientName { get; set; }
        public string? PatientEmail { get; set; }
    }
}
