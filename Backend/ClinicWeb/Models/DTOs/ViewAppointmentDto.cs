using System;
using ClinicWeb.Models.Entities;

namespace ClinicWeb.Models.DTOs
{
    public class ViewAppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        
        // Doctor info
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        
        // Clinic info
        public int ClinicId { get; set; }
        public string? ClinicName { get; set; }
        
        // Category info
        public int AppointmentCategoryId { get; set; }
        public string? CategoryName { get; set; }
        
        // Appointment details
        public string? Notes { get; set; }
        public string? Status { get; set; }  // "Scheduled", "Completed", "Cancelled"
    }
}