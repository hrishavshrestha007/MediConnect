using System;

namespace ClinicWeb.Models.DTOs
{
    public class CancelAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string? Status { get; set; }
    }
}
