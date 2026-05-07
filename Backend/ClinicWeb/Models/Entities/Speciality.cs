using System;
using System.Text.Json.Serialization;

namespace ClinicWeb.Models.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // e.g., "Cardiology"

        [JsonIgnore]  
        public List<Doctor>? Doctors { get; set; }
    }
}
