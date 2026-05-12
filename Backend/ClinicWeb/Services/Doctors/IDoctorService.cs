using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Shared;

namespace ClinicWeb.Services.Doctors
{
    public interface IDoctorService : ICrudService<Doctor, int>
    {
        Task<IEnumerable<DoctorSearchResultDto>> SearchAsync(string searchTerm);
    }
}
