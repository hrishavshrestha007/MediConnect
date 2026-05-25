using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/patients")] // route for patients
    [ApiController] // api controller
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService; // service for patients
        public PatientController(IPatientService patientService) // constructor for patients
        {
            _patientService = patientService; // initialize patient service
        }

        /// <summary>
        /// Get all patients. Requires authentication.
        /// </summary>
        /// <returns>A list of all patients.</returns>

        [HttpGet] // get all patients
        [Authorize] // require authentication to get all patients
        public async Task<IActionResult> GetAll() // get all patients
        {
            var patients = await _patientService.GetAllAsync(); // get all patients from service
            if (patients == null || !patients.Any()) // if no patients found
                return NotFound("No patients found."); // return not found with message
            return Ok(patients); // return ok with patients
        }

        /// <summary>
        /// Get a patient by ID. Requires authentication.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>The patient with the specified ID.</returns>

        [HttpGet("{id}")] // get patient by id
        [Authorize] // require authentication to get patient by id
        public async Task<IActionResult> GetById(int id) // get patient by id
        {
            var nameidClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier); // get nameid claim from user
            if (nameidClaim == null || !int.TryParse(nameidClaim.Value, out int userId)) // if nameid claim is missing or invalid
                return Unauthorized(); // return unauthorized if user is not authenticated

            // Only allow users to view their own profile
            if (userId != id) // if user id does not match requested patient id
                return Forbid(); // return forbidden if user tries to access another patient's profile

            try
            {
                var patient = await _patientService.GetByIdAsync(id); // get patient by id from service

                // Return as anonymous object with all fields
                return Ok(new
                {
                    patient.Id,
                    patient.FirstName,
                    patient.LastName,
                    patient.Email,
                    patient.SocialSecurityNumber,
                    patient.Birthdate,
                    patient.Gender,
                    patient.TaxNumber,
                    patient.Religion,
                    patient.DriverLicenseNumber,
                    patient.MedicalInsuranceMemberId
                });
            }
            catch (KeyNotFoundException) // if patient not found
            {
                return NotFound(); // return not found without message to avoid exposing existence of patient
            }
        }

        /// <summary>
        /// Get all registered patients. Requires authentication.
        /// </summary>
        /// <returns>A list of all registered patients.</returns>

        [HttpGet("registered")] // get registered patients
        [Authorize] // require authentication to get registered patients
        public async Task<IActionResult> GetRegisteredPatients() // get registered patients
        {
            var patients = await _patientService.GetRegisteredPatientsAsync(); // get registered patients from service
            if (patients == null || !patients.Any()) // if no registered patients found
                return NotFound("No registered patients found."); // return not found with message
            return Ok(patients); // return ok with registered patients
        }

        /// <summary>
        /// Create a new patient.
        /// </summary>
        /// <param name="request">The registration request containing patient information.</param>
        /// <returns>A confirmation message indicating successful registration.</returns>

        [HttpPost] // create new patient
        public async Task<IActionResult> Create([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.FirstName) || // validate required fields
                string.IsNullOrEmpty(request.LastName) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "First name, last name, email, and password are required." }); // return bad request if required fields are missing
            }

            try
            {
                await _patientService.RegisterAsync(request); // register patient from service
                return CreatedAtAction(nameof(Create), new { message = "Patient registered successfully." }); // return created at action with message
            }
            catch (InvalidOperationException ex) // if conflict occurs (e.g., email already in use)
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
            catch (ArgumentException ex) // if validation fails
            {
                return BadRequest(new { message = ex.Message }); // return bad request with message if validation fails
            }
        }

        /// <summary>
        /// Update an existing patient's information. Requires authentication.
        /// </summary>
        /// <returns>A confirmation message indicating successful update.</returns>
        //PUT api/patient/{id}

        [HttpPut("{id}")] // update patient by id
        [Authorize] // require authentication to update patient
        public async Task<IActionResult> Update(int id, [FromBody] Patient updatedPatient)
        {
            try
            {
                await _patientService.UpdateAsync(id, updatedPatient); // update patient from service
                return Ok(new { message = "Patient updated successfully." }); // return ok with message on successfully updated
            }
            catch (KeyNotFoundException ex) // if patient not found
            {
                return NotFound(new { message = ex.Message }); // return not found
            }
            catch (InvalidOperationException ex) // if update fails due to conflict (e.g., email already in use by another patient)
            {
                return Conflict(new { message = ex.Message }); // return conflict
            }
            catch (ArgumentException ex) // if validation fails
            {
                return BadRequest(new { message = ex.Message }); // return bad request
            }
        }

        /// <summary>
        /// Delete a patient by ID. Requires authentication.
        /// </summary>
        /// <returns>A confirmation message indicating successful deletion.</returns>
        //DELETE api/patient/{id}

        [HttpDelete("{id}")] // delete patient by id
        [Authorize] // require authentication to delete patient
        public async Task<IActionResult> Delete(int id) // delete patient by id
        {
            try
            {
                await _patientService.DeleteAsync(id); // delete patient from service
                return NoContent(); // return no content on successfully deleted
            }
            catch (Exception) // if patient not found or has associated appointments
            {
                return NotFound(new { message = "Patient not found." }); // return not found
            }
        }
    }
}
