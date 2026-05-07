using System;

namespace ClinicWeb.Models.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        // When is the appointment?
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }

        // Foreign keys
        public int DoctorId { get; set; }
        public int ClinicId { get; set; }
        public int UserId { get; set; }
        public int AppointmentCategoryId { get; set; }

        // Optional: notes or status
        public string? Notes { get; set; }
        public AppointmentStatus Status { get; set; }

        public enum AppointmentStatus
        {
            Scheduled,
            Completed,
            Cancelled
        }

        // Navigation
        public Doctor? Doctor { get; set; }
        public Patient? User { get; set; }
        public AppointmentCategory? Category { get; set; }
    }


}
