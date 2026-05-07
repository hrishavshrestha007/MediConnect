using System;

namespace ClinicWeb.Models.DTOs
{
    public class PatientAppointmentListDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? DoctorName { get; set; }
        public string? ClinicName { get; set; }
        public string? SpecialityName { get; set; }
    }
}
