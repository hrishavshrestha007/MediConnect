using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Get all patients. Requires authentication.
        /// </summary>
        /// <returns>A list of all patients.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            if (patients == null || !patients.Any())
                return NotFound("No patients found.");
            return Ok(patients);
        }

        /// <summary>
        /// Get a patient by ID. Requires authentication.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>The patient with the specified ID.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(id);
                return Ok(patient);
            }
            catch (Exception)
            {
                return NotFound("Patient not found.");
            }
        }

        /// <summary>
        /// Get all registered patients. Requires authentication.
        /// </summary>
        /// <returns>A list of all registered patients.</returns>
        [HttpGet("registered")]
        [Authorize]
        public async Task<IActionResult> GetRegisteredPatients()
        {
            var patients = await _patientService.GetRegisteredPatientsAsync();
            if (patients == null || !patients.Any())
                return NotFound("No registered patients found.");
            return Ok(patients);
        }

        /// <summary>
        /// Create a new patient.
        /// </summary>
        /// <param name="request">The registration request containing patient information.</param>
        /// <returns>A confirmation message indicating successful registration.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.FirstName) ||
                string.IsNullOrEmpty(request.LastName) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "First name, last name, email, and password are required." });
            }

            try
            {
                await _patientService.RegisterAsync(request);
                return Ok(new { message = "Patient registered successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing patient's information. Requires authentication.
        /// </summary>
        /// <returns>A confirmation message indicating successful update.</returns>
        //PUT api/patient/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Patient updatedPatient)
        {
            try
            {
                await _patientService.UpdateAsync(id, updatedPatient);
                return Ok(new { message = "Patient updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a patient by ID. Requires authentication.
        /// </summary>
        /// <returns>A confirmation message indicating successful deletion.</returns>
        //DELETE api/patient/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _patientService.DeleteAsync(id);
                return Ok(new { message = "Patient deleted successfully." });
            }
            catch (Exception)
            {
                return NotFound(new { message = "Patient not found." });
            }
        }
    }
}
