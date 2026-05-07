using System;

namespace ClinicWeb.Models.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public List<Doctor>? Doctors { get; set; }
    }
}
