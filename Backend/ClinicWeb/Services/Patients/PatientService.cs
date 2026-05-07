using ClinicWeb.Data;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicWeb.Services
{
    public class PatientService : IPatientService
    {
        private readonly ClinicDbContext _context;

        public PatientService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.Appointments)
                .ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            return patient;
        }

        public async Task<Patient> GetByEmailAsync(string email)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Email == email);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with email {email} not found.");

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetRegisteredPatientsAsync()
        {
            return await _context.Patients
                .Where(p => p.IsRegistered)
                .Include(p => p.Appointments)
                .ToListAsync();
        }

        public async Task CreateAsync(Patient patient)
        {
            bool exists = await _context.Patients
                .AnyAsync(p => p.Email == patient.Email);

            if (exists)
                throw new InvalidOperationException("A patient with this email already exists.");

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterAsync(RegisterRequestDto request)
        {
            bool exists = await _context.Patients
                .AnyAsync(p => p.Email == request.Email);

            if (exists)
                throw new InvalidOperationException("A patient with this email already exists.");

            var patient = new Patient
            {
                FirstName = request.FirstName ?? string.Empty,
                LastName = request.LastName ?? string.Empty,
                Email = request.Email ?? string.Empty,
                SocialSecurityNumber = request.SocialSecurityNumber,
                Birthdate = request.Birthdate,
                Gender = request.Gender,
                TaxNumber = request.TaxNumber,
                Religion = request.Religion,
                DriverLicenseNumber = request.DriverLicenseNumber,
                MedicalInsuranceMemberId = request.MedicalInsuranceMemberId
            };

            var hasher = new PasswordHasher<Patient>();
            patient.PasswordHash = hasher.HashPassword(patient, request.Password!);

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Patient updated)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            // Non-sensitive fields — safe to update for both guests and registered
            patient.FirstName = updated.FirstName;
            patient.LastName = updated.LastName;
            patient.Email = updated.Email;

            // Sensitive fields — only update if this is a registered patient
            if (patient.IsRegistered)
            {
                patient.SocialSecurityNumber = updated.SocialSecurityNumber;
                patient.Birthdate = updated.Birthdate;
                patient.Gender = updated.Gender;
                patient.TaxNumber = updated.TaxNumber;
                patient.Religion = updated.Religion;
                patient.DriverLicenseNumber = updated.DriverLicenseNumber;
                patient.MedicalInsuranceMemberId = updated.MedicalInsuranceMemberId;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            if (patient.Appointments != null && patient.Appointments.Any())
                throw new InvalidOperationException("Cannot delete a patient that still has appointments.");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}