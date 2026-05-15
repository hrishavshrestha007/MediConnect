using System;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Shared;

namespace ClinicWeb.Services.Appointments
{
    public interface IAppointmentService
    {
        Task CancelAsync(int patientId, CancelAppointmentDto request);
        Task MoveAsync(int patientId, MoveAppointmentDto request);
        Task BookAsync(BookAppointmentRequestDto request, int? patientId = null);

        Task<IEnumerable<ViewAppointmentDto>> GetPatientAppointmentsAsync(int patientId);

    }
}
