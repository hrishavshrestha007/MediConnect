using System;

namespace ClinicWeb.Models.Config
{
    public class JwtSetting
{
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}
}