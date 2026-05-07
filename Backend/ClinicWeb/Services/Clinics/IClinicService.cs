using System;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Shared;

namespace ClinicWeb.Services.Clinics
{
    public interface IClinicService : ICrudService<Clinic, int>
    {
        Task<IEnumerable<Doctor>> GetDoctorsByClinicId(int clinicId);

    }
}
