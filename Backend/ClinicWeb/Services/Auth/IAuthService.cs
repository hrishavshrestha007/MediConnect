using System;
using ClinicWeb.Models.Entities;

namespace ClinicWeb.Services.Auth
{
    public interface IAuthService
    {
        public Task<bool> ValidateUserAsync(string username, string password);
        public Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password);
        public Task<Patient?> GetUserByUsernameAsync(string username);
        public string GenerateToken(Patient user);
    }
}