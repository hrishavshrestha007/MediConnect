using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicWeb.Models.Entities;
using ClinicWeb.Models.DTOs;

namespace ClinicWeb.Services.AppointmentCategories
{
    public interface IAppointmentCategoryServices
    {
        Task<IEnumerable<AppointmentCategory>> GetAllAsync();
        Task<AppointmentCategory> GetByIdAsync(int id);
        

    }
}