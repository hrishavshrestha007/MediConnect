using Microsoft.AspNetCore.Mvc; // using mvc
using ClinicWeb.Models.Entities; // using entities
using ClinicWeb.Services.AppointmentCategories; // using appointment categories service


namespace ClinicWeb.Controllers
{
    [Route("api/appointmentcategories")] // route for appointment categories
    [ApiController] // api controller
    public class AppointmentCategoryController : ControllerBase // class for appointment categories
    {
        private readonly IAppointmentCategoryServices _appointmentCategoryServices; // service for appointment categories

        public AppointmentCategoryController(IAppointmentCategoryServices appointmentCategoryServices) // constructor for appointment categories
        {
            _appointmentCategoryServices = appointmentCategoryServices;
        }

        /// <summary>
        /// Retrieves all appointment categories or a specific category by ID. 
        /// </summary>
        /// <returns>A list of appointment categories or a specific category by ID.</returns>
        [HttpGet] // get all appointment categories
        public async Task<ActionResult<IEnumerable<AppointmentCategory>>> GetAll()
        {
            try
            {
                var categories = await _appointmentCategoryServices.GetAllAsync();
                
                if (categories == null || !categories.Any()) // if no appointment categories found
                {
                    return NotFound("No appointment categories found."); // return not found
                }
                
                return Ok(categories); // return appointment categories
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // return internal server error
            }
        }

        /// <summary>
        /// Retrieves a specific appointment category by its ID. 
        /// </summary>
 
        /// <returns></returns>

        // GET: api/AppointmentCategory/{id}
        [HttpGet("{id}")] // get appointment category by id
        public async Task<ActionResult<AppointmentCategory>> GetById(int id)
        {
            try // try to get appointment category by id
            {
                var category = await _appointmentCategoryServices.GetByIdAsync(id);
                
                if (category == null) // if appointment category not found
                {
                    return NotFound($"Appointment category with ID {id} not found."); // return not found
                }
                
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // return internal server error
            }
        }
    }
}