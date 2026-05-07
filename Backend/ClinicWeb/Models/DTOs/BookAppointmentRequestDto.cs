using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicWeb.Models.DTOs
{
    public class BookAppointmentRequestDto
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        public int AppointmentCategoryId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
