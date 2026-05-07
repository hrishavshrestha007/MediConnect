using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicWeb.Models.DTOs
{
    public class SearchDoctorRequestDto
    {
        public string? SearchTerm { get; set; }
    }
}
