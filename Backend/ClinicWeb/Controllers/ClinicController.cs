using ClinicWeb.Data;
using ClinicWeb.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicWeb.Services.Clinics;

namespace ClinicWeb.Controllers
{
    [Route("api/clinics")] // route for clinics
    [ApiController] // api controller
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService; // service for clinics
        public ClinicController(IClinicService _clinicService) // constructor for clinics
        {
            this._clinicService = _clinicService; // initialize clinic service
        }

        /// <summary>
        /// Get all clinics.
        /// </summary>
        /// <returns> A list of all clinics. </returns>

        //GET api/clinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetAllClinics() // get all clinics
        {
            var clinics = await _clinicService.GetAllAsync(); // get all clinics from service
            if (clinics == null || !clinics.Any()) // if no clinics found
                return NotFound("No clinics found."); // return not found
            return Ok(clinics); // return ok with clinics
        }

        /// <summary>
        /// Get a clinic by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //GET api/clinic/{id}
        [HttpGet("{id}")] // get clinic by id
        public async Task<ActionResult<Clinic>> GetClinicById(int id) // get clinic by id
        {
            try
            {
                var clinic = await _clinicService.GetByIdAsync(id); // get clinic by id from service
                return Ok(clinic); // return ok with clinic
            }
            catch (KeyNotFoundException ex) // if clinic not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
        }

        /// <summary>
        /// Get all doctors associated with a specific clinic.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //GET api/clinic/{id}/doctors
        [HttpGet("{id}/doctors")] // get doctors by clinic id
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsByClinicId(int id) // get doctors by clinic id
        {
            try
            {
                var doctors = await _clinicService.GetDoctorsByClinicId(id); // get doctors by clinic id from service
                return Ok(doctors); // return ok with doctors
            }
            catch (KeyNotFoundException ex) // if clinic not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
        }

        /// <summary>
        /// Create a new clinic.
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>

        //POST api/clinic
        [HttpPost] // create new clinic
        public async Task<ActionResult> CreateClinic([FromBody] Clinic clinic) // create new clinic from body
        {
            try // try to create clinic
            {
                await _clinicService.CreateAsync(clinic); // create clinic from service
                return CreatedAtAction(nameof(GetClinicById), new { id = clinic.Id }, clinic); // return created at action with clinic
            }
            catch (InvalidOperationException ex) // if clinic already exists
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }

        /// <summary>
        /// Update an existing clinic. The ID in the URL must match the ID in the body of the request.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedClinic"></param>
        /// <returns> Returns 204 update successful. </returns>

        //PUT api/clinic/{id}
        [HttpPut("{id}")] // update clinic by id
        public async Task<ActionResult> UpdateClinic(int id, [FromBody] Clinic updatedClinic) // update clinic by id from body
        {
            try
            {
                // Validate FIRST (before updating)
                if (id != updatedClinic.Id) // if id in url does not match id in body
                {
                    return BadRequest(new { message = "ID in the URL does not match ID in the body." }); // return bad request with message
                }

                // Then update
                await _clinicService.UpdateAsync(id, updatedClinic); // update clinic from service

                // Return either:
                return NoContent(); 
            }
            catch (KeyNotFoundException ex) // if clinic not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
            catch (InvalidOperationException ex) // if clinic already exists
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }

        /// <summary>
        /// Delete a clinic by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns 204 No Content on successfully deleted. </returns>

        //DELETE api/clinic/{id}
        [HttpDelete("{id}")] // delete clinic by id
        public async Task<ActionResult> DeleteClinic(int id) // delete clinic by id
        {
            try
            {
                await _clinicService.DeleteAsync(id); // delete clinic from service
                if (id <= 0) // if id is invalid
                {
                    return BadRequest(new { message = "Invalid clinic ID." }); // return bad request with message
                }
                return NoContent(); // return no content on successfully deleted
            }
            catch (InvalidOperationException ex) // if clinic has associated doctors or appointments
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }
    }
}
