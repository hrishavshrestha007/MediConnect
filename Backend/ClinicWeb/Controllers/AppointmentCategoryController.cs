using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.AppointmentCategories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWeb.Controllers
{
    [Route("api/appointmentcategories")]
    [ApiController]
    public class AppointmentCategoryController : ControllerBase
    {
        private readonly IAppointmentCategoryServices _appointmentCategoryServices;

        public AppointmentCategoryController(IAppointmentCategoryServices appointmentCategoryServices)
        {
            _appointmentCategoryServices = appointmentCategoryServices;
        }

        /// <summary>
        /// Retrieves all appointment categories or a specific category by ID. 
        /// <returns>A list of appointment categories or a specific category by ID.</returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentCategory>>> GetAll()
        {
            try
            {
                var categories = await _appointmentCategoryServices.GetAllAsync();
                
                if (categories == null || !categories.Any())
                {
                    return NotFound("No appointment categories found.");
                }
                
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific appointment category by its ID. 
        /// </summary>
 
        /// <returns></returns>

        // GET: api/AppointmentCategory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentCategory>> GetById(int id)
        {
            try
            {
                var category = await _appointmentCategoryServices.GetByIdAsync(id);
                
                if (category == null)
                {
                    return NotFound($"Appointment category with ID {id} not found.");
                }
                
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}