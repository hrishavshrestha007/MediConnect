using System;
using ClinicWeb.Data;
using ClinicWeb.Models.DTOs;
using static ClinicWeb.Models.Entities.Appointment;
using Microsoft.EntityFrameworkCore;
using ClinicWeb.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClinicWeb.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {

        private readonly ClinicDbContext _context;

        public AppointmentService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task BookAsync(BookAppointmentRequestDto request, int? patientId = null)
        {
            if (patientId == null)
                throw new ArgumentException("Patient ID is required to book an appointment.");

            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found.");

            var category = await _context.AppointmentCategories.FindAsync(request.AppointmentCategoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            var newEnd = request.AppointmentDate.AddMinutes(request.DurationMinutes);

            bool conflict = await _context.Appointments
                .Where(a => a.DoctorId == request.DoctorId && a.Status != AppointmentStatus.Cancelled)
                .AnyAsync(a =>
                    request.AppointmentDate < a.AppointmentDate.AddMinutes(a.DurationMinutes) &&
                    newEnd > a.AppointmentDate);

            if (conflict)
                throw new InvalidOperationException("The doctor is not available at the requested time.");

            // Check for patient appointment conflicts at the same clinic
            bool patientConflict = await _context.Appointments
                .Where(a => a.UserId == patientId
                    && a.Status != AppointmentStatus.Cancelled
                    && a.Doctor!.ClinicId == doctor.ClinicId)
                .AnyAsync(a =>
                    request.AppointmentDate < a.AppointmentDate.AddMinutes(a.DurationMinutes) &&
                    newEnd > a.AppointmentDate);

            if (patientConflict)
                throw new InvalidOperationException("You already have an appointment at this clinic at this time.");

            var appointment = new Appointment
            {
                UserId = patientId.Value,
                DoctorId = request.DoctorId,
                ClinicId = doctor.ClinicId,
                AppointmentCategoryId = request.AppointmentCategoryId,
                AppointmentDate = request.AppointmentDate,
                DurationMinutes = request.DurationMinutes,
                Status = AppointmentStatus.Scheduled
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task CancelAsync(int patientId, CancelAppointmentDto request)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId && a.UserId == patientId);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found or does not belong to you.");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("This appointment is already cancelled.");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new InvalidOperationException("Cannot cancel a completed appointment.");

            appointment.Status = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();
        }

        public async Task MoveAsync(int patientId, MoveAppointmentDto request)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId && a.UserId == patientId);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found or does not belong to you.");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Cannot move a cancelled appointment.");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new InvalidOperationException("Cannot move a completed appointment.");

            var newEnd = request.NewDate.AddMinutes(appointment.DurationMinutes);

            bool conflict = await _context.Appointments
                .Where(a => a.DoctorId == appointment.DoctorId
                         && a.Id != appointment.Id
                         && a.Status != AppointmentStatus.Cancelled)
                .AnyAsync(a =>
                    request.NewDate < a.AppointmentDate.AddMinutes(a.DurationMinutes) &&
                    newEnd > a.AppointmentDate);

            if (conflict)
                throw new InvalidOperationException("The doctor is not available at the requested time.");

            appointment.AppointmentDate = request.NewDate;
            appointment.Status = AppointmentStatus.Scheduled;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserId == patientId)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Speciality)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Clinic)
                .Include(a => a.Category)
                .ToListAsync();

            return appointments
                .Where(a => a.Doctor != null)  // Filter out null doctors
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    DurationMinutes = a.DurationMinutes,
                    Status = a.Status.ToString(),
                    DoctorName = $"Dr. {a.Doctor!.FirstName} {a.Doctor.LastName}",
                    DoctorSpeciality = a.Doctor.Speciality?.Name,
                    ClinicName = a.Doctor.Clinic?.Name,
                    CategoryName = a.Category?.Name
                }).ToList();
        }

        public async Task<AppointmentDto> GetByIdAsync(int patientId, int appointmentId)
        {
            var appointment = await _context.Appointments
                .Where(a => a.UserId == patientId && a.Id == appointmentId)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Speciality)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Clinic)
                .Include(a => a.Category)
                .FirstOrDefaultAsync();

            if (appointment == null || appointment.Doctor == null)
                throw new KeyNotFoundException("Appointment not found or does not belong to you.");

            return new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                DurationMinutes = appointment.DurationMinutes,
                Status = appointment.Status.ToString(),
                DoctorName = $"Dr. {appointment.Doctor.FirstName} {appointment.Doctor.LastName}",
                DoctorSpeciality = appointment.Doctor.Speciality?.Name,
                ClinicName = appointment.Doctor.Clinic?.Name,
                CategoryName = appointment.Category?.Name
            };
        }

        public async Task<IEnumerable<PatientAppointmentListDto>> GetByPatientAppointmentsAsync(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserId == patientId)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Speciality)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Clinic)
                .Include(a => a.Category)
                .ToListAsync();

            return appointments
                .Where(a => a.Doctor != null)  // Filter out null doctors
                .Select(a => new PatientAppointmentListDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status.ToString(),
                    DoctorName = $"Dr. {a.Doctor!.FirstName} {a.Doctor.LastName}",
                    ClinicName = a.Doctor.Clinic?.Name,
                    SpecialityName = a.Doctor.Speciality?.Name
                }).ToList();
        }

        public async Task<IEnumerable<ViewAppointmentDto>> GetPatientAppointmentsAsync(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserId == patientId)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Speciality)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d!.Clinic)
                .Include(a => a.Category)
                .ToListAsync();

            return appointments
                .Where(a => a.Doctor != null)
                .Select(a => new ViewAppointmentDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    DurationMinutes = a.DurationMinutes,
                    Status = a.Status.ToString(),
                    DoctorId = a.DoctorId,
                    DoctorName = $"Dr. {a.Doctor!.FirstName} {a.Doctor.LastName}",
                    ClinicId = a.Doctor.ClinicId,
                    ClinicName = a.Doctor.Clinic?.Name,
                    AppointmentCategoryId = a.AppointmentCategoryId,
                    CategoryName = a.Category?.Name,
                    Notes = a.Notes
                }).ToList();
        }


    }
}
