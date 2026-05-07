using System;

namespace ClinicWeb.Models.Entities
{
    public class AppointmentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // e.g., "Consultation", "Follow-up"
        public int? DefaultDurationMinutes { get; set; }  // Optional default duration

        public List<Appointment>? Appointments { get; set; }
    }
}
