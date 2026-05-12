using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicWeb.Data;
using ClinicWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicWeb.Services.AppointmentCategories
{
    public class AppointmentCategoryServices : IAppointmentCategoryServices
    {
        private readonly ClinicDbContext _context;

        public AppointmentCategoryServices(ClinicDbContext appointmentCategoryServices)
        {
            _context = appointmentCategoryServices;
        }

        public async Task<IEnumerable<AppointmentCategory>> GetAllAsync()
        {
            return await _context.AppointmentCategories.ToListAsync();
        }

        public async Task<AppointmentCategory> GetByIdAsync(int id)
        {
            return await _context.AppointmentCategories.FindAsync(id);
        }

    }
}