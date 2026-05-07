using System;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Shared;

namespace ClinicWeb.Services.Patients
{
    public interface IPatientService
    {
        Task<Patient> GetByEmailAsync(string email);
        Task<IEnumerable<Patient>> GetRegisteredPatientsAsync();

        Task RegisterAsync(RegisterRequestDto request);
        Task UpdateAsync(int id, Patient updated);
        Task DeleteAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task CreateAsync(Patient patient);

        

    }
}
