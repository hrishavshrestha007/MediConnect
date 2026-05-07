using System;
using ClinicWeb.Data;
using ClinicWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicWeb.Services.Clinics
{
    public class ClinicService : IClinicService
    {

        private readonly ClinicDbContext _context;
        public ClinicService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _context.Clinics
                .Include(c => c.Doctors)
                .ToListAsync();
        }

        public async Task<Clinic> GetByIdAsync(int id)
        {
            var clinic = await _context.Clinics
                .Include(c => c.Doctors)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with ID {id} not found.");

            return clinic;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByClinicId(int clinicId)
        {
            var clinic = await _context.Clinics
                .Include(c => c.Doctors)
                .FirstOrDefaultAsync(c => c.Id == clinicId);

            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");

            return clinic.Doctors ?? new List<Doctor>();
        }

        public async Task CreateAsync(Clinic clinic)
        {
            bool exists = await _context.Clinics
                .AnyAsync(c => c.Name == clinic.Name && c.Address == clinic.Address);

            if (exists)
                throw new InvalidOperationException("A clinic with this name and address already exists.");

            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Clinic updated)
        {
            var clinic = await _context.Clinics.FindAsync(id);

            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with ID {id} not found.");

            clinic.Id = id; // Ensure the ID remains unchanged
            clinic.Name = updated.Name;
            clinic.Address = updated.Address;
            clinic.PhoneNumber = updated.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var clinic = await _context.Clinics
                .Include(c => c.Doctors)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with ID {id} not found.");

            if (clinic.Doctors != null && clinic.Doctors.Any())
                throw new InvalidOperationException("Cannot delete a clinic that still has doctors assigned to it.");

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
        }
    }
}
