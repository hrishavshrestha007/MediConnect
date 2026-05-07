using ClinicWeb.Data;
using ClinicWeb.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicWeb.Services.Clinics;

namespace ClinicWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        public ClinicController(IClinicService _clinicService)
        {
            this._clinicService = _clinicService;
        }

        /// <summary>
        /// Get all clinics.
        /// </summary>
        /// <returns> A list of all clinics. </returns>

        //GET api/clinic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetAllClinics()
        {
            var clinics = await _clinicService.GetAllAsync();
            if (clinics == null || !clinics.Any())
                return NotFound("No clinics found.");
            return Ok(clinics);
        }

        /// <summary>
        /// Get a clinic by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //GET api/clinic/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> GetClinicById(int id)
        {
            try
            {
                var clinic = await _clinicService.GetByIdAsync(id);
                return Ok(clinic);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all doctors associated with a specific clinic.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //GET api/clinic/{id}/doctors
        [HttpGet("{id}/doctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsByClinicId(int id)
        {
            try
            {
                var doctors = await _clinicService.GetDoctorsByClinicId(id);
                return Ok(doctors);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new clinic.
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>

        //POST api/clinic
        [HttpPost]
        public async Task<ActionResult> CreateClinic([FromBody] Clinic clinic)
        {
            try
            {
                await _clinicService.CreateAsync(clinic);
                return CreatedAtAction(nameof(GetClinicById), new { id = clinic.Id }, clinic);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing clinic. The ID in the URL must match the ID in the body of the request.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedClinic"></param>
        /// <returns> Returns 204 update successful. </returns>

        //PUT api/clinic/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClinic(int id, [FromBody] Clinic updatedClinic)
        {
            try
            {
                // Validate FIRST (before updating)
                if (id != updatedClinic.Id)
                {
                    return BadRequest(new { message = "ID in the URL does not match ID in the body." });
                }

                // Then update
                await _clinicService.UpdateAsync(id, updatedClinic);

                // Return either:
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a clinic by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns 204 No Content on successfully deleted. </returns>

        //DELETE api/clinic/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClinic(int id)
        {
            try
            {
                await _clinicService.DeleteAsync(id);
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid clinic ID." });
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
