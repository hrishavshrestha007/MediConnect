using System;
using System.Text.Json.Serialization;
namespace ClinicWeb.Models.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Foreign keys
        public int SpecialityId { get; set; }
        public int ClinicId { get; set; }

        // Navigation
        public Speciality? Speciality { get; set; }
        [JsonIgnore]
        public Clinic? Clinic { get; set; }
        [JsonIgnore]
        public List<Appointment>? Appointments { get; set; }
    }
}
