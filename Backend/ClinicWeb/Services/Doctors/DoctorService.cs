using ClinicWeb.Data;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Shared;
using Microsoft.EntityFrameworkCore;

namespace ClinicWeb.Services.Doctors
{
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _context;

        public DoctorService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .Include(d => d.Clinic)
                .ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Speciality)
                .Include(d => d.Clinic)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");

            return doctor;
        }

        // Search by first or last name — required by the brief
        public async Task<IEnumerable<DoctorSearchResultDto>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty.");

            var term = searchTerm.Trim().ToLower();

            var results = await _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.FirstName.ToLower().Contains(term) ||
                            d.LastName.ToLower().Contains(term))
                .Select(d => new DoctorSearchResultDto
                {
                    FullName = $"Dr. {d.FirstName} {d.LastName}",
                    SpecialityName = d.Speciality.Name,
                    ClinicName = _context.Clinics
                                       .Where(c => c.Id == d.ClinicId)
                                       .Select(c => c.Name)
                                       .FirstOrDefault()
                })
                .ToListAsync();

            if (!results.Any())
                throw new KeyNotFoundException($"No doctors found matching '{searchTerm}'.");

            return results;
        }

        public async Task CreateAsync(Doctor doctor)
        {
            bool exists = await _context.Doctors
                .AnyAsync(d => d.FirstName == doctor.FirstName &&
                               d.LastName == doctor.LastName &&
                               d.SpecialityId == doctor.SpecialityId &&
                               d.ClinicId == doctor.ClinicId);

            if (exists)
                throw new InvalidOperationException("This doctor is already registered at this clinic.");

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Doctor updated)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");

            doctor.FirstName = updated.FirstName;
            doctor.LastName = updated.LastName;
            doctor.SpecialityId = updated.SpecialityId;
            doctor.ClinicId = updated.ClinicId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");

            if (doctor.Appointments != null && doctor.Appointments.Any())
                throw new InvalidOperationException("Cannot delete a doctor that has existing appointments.");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}