using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicWeb.Data;
using ClinicWeb.Models.Config;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClinicWeb.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ClinicDbContext _context;
        private readonly JwtSetting _jwtSettings;
        private readonly PasswordHasher<Patient> _passwordHasher;

        public AuthService(ClinicDbContext context, JwtSetting jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _passwordHasher = new PasswordHasher<Patient>();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Patients
                .SingleOrDefaultAsync(patient => patient.Email == username);

            if (user == null || user.PasswordHash == null)
            {
                return false;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            return verificationResult == PasswordVerificationResult.Success;
        }

        public async Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password)
        {
            var emailExists = await _context.Patients
                .AnyAsync(patient => patient.Email == email);

            if (emailExists)
            {
                return false;
            }

            var user = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _context.Patients.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Patient?> GetUserByUsernameAsync(string username)
        {
            return await _context.Patients
                .SingleOrDefaultAsync(patient => patient.Email == username);
        }

        public string GenerateToken(Patient patient)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
                throw new InvalidOperationException("JWT SecretKey is not configured.");
                
            // Create the claims to store inside the token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patient.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, patient.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Convert the secret key text into bytes and build a signing key
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var securityKey = new SymmetricSecurityKey(secretKeyBytes);

            // Choose the signing algorithm used to sign the token
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            // Decide when the token should expire
            var expiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            // Build the JWT token object
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiryTime,
                signingCredentials: signingCredentials
            );

            // Convert the token object into a string that can be returned to the client
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}