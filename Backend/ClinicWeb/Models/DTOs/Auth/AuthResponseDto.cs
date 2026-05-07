using System;

namespace ClinicWeb.Models.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
    }
}
