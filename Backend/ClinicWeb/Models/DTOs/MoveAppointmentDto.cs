using System;

namespace ClinicWeb.Models.DTOs
{
    public class MoveAppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime NewDate { get; set; }
    }
}
