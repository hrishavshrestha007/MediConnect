using ClinicWeb.Models.DTOs; // using dtos
using ClinicWeb.Services.Appointments;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Authorization; // using authorization
using Microsoft.AspNetCore.Mvc; // using mvc
using System.Security.Claims; // using claims
using ClinicWeb.Models.Entities; // using entities

namespace ClinicWeb.Controllers
{
    [Route("api/appointment")] // route for appointments
    [ApiController] // api controller
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService; // service for appointments
        private readonly IPatientService _patientService; // service for patients

        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
        }

        /// <summary>
        /// Book an appointment. If the user is logged in, the appointment will be associated with their account. If the user is a guest, a new patient record will be created for them and the appointment will be associated with that record.
        /// </summary>
        /// <returns> Returns a confirmation message indicating successful booking.</returns>

        [HttpPost("book")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentRequestDto request)
        {
            try
            {
                int? patientId = GetPatientId(); // null if guest, ID if logged in

                if (patientId == null)
                {
                    // Guest booking
                    if (string.IsNullOrEmpty(request.FirstName) ||
                        string.IsNullOrEmpty(request.LastName) ||
                        string.IsNullOrEmpty(request.Email))
                    {
                        return BadRequest(new { message = "For guest bookings, firstName, lastName, and email are required." });
                    }

                    // Check if email already exists
                    var existingPatient = await _patientService.GetByEmailAsync(request.Email);
                    if (existingPatient != null)
                    {
                        // Patient already exists, use their ID
                        patientId = existingPatient.Id;
                    }
                    else
                    {
                        // Create new guest patient
                        var guestPatient = new Patient
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                            Birthdate = request.Birthdate
                        };

                        await _patientService.CreateAsync(guestPatient); // create new guest patient
                        patientId = guestPatient.Id;
                    }
                }

                await _appointmentService.BookAsync(request, patientId); // book appointment
                return CreatedAtAction(nameof(Book), new { message = "Appointment booked successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // return not found
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); // return conflict
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // return bad request
            }
        }

        /// <summary>
        /// Move an existing appointment to a new date and time. Only the patient who booked the appointment can move it, and they must be logged in to do so.
        /// </summary>
        /// <returns> Returns a confirmation message indicating successful rescheduling.</returns>

        [HttpPut("move")]
        [Authorize] // Only logged-in users can move appointments
        public async Task<IActionResult> Move([FromBody] MoveAppointmentDto request) // move appointment
        {
            try
            {
                int? patientId = GetPatientId(); // get patient id
                if (patientId == null) // if patient id is null
                    return Unauthorized(new { message = "You must be logged in to move an appointment." }); // return unauthorized

                await _appointmentService.MoveAsync(patientId.Value, request); // move appointment
                return Ok(new { message = "Appointment moved successfully." }); // return ok
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); // return bad request
            }
        }

        /// <summary>
        /// Cancel an appointment. Only the patient who booked the appointment can cancel it, and they must be logged in to do so.
        /// </summary>
        /// <returns> Returns a confirmation message indicating successful cancellation.</returns>

        [HttpPut("{id}/cancel")]
        [Authorize] // Only logged-in users can cancel appointments
        public async Task<IActionResult> Cancel(int id) // cancel appointment
        {
            var patientId = GetPatientId(); // get patient id
            Console.WriteLine($"Canceling appointment {id} for patient {patientId}"); // log canceling appointment
            if (patientId == null)
                return Unauthorized(new { message = "You must be logged in to cancel an appointment." }); // return unauthorized

            var request = new CancelAppointmentDto { AppointmentId = id }; // create cancel appointment dto

            try
            {
                await _appointmentService.CancelAsync(patientId.Value, request); // cancel appointment
                return Ok(new { message = "Appointment cancelled successfully.", 
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // return not found
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); // return conflict
            }

        }

        /// <summary>
        /// Get all appointments for the logged-in patient.
        /// </summary>
        /// <returns> Returns a list of appointments for the logged-in patient.</returns>

        [HttpGet("myappointments")]
        [Authorize]
        public async Task<IActionResult> GetMyAppointments() // get my appointments
        {
            var patientId = GetPatientId(); // get patient id
            if (patientId == null)
                return Unauthorized(new { message = "You must be logged in." }); // return unauthorized

            var appointments = await _appointmentService.GetPatientAppointmentsAsync(patientId.Value); // get patient appointments
            return Ok(appointments);
        }

        private int? GetPatientId() // get patient id
        {
            if (User.Identity!.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // get user id claim
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int patientId)) // if user id claim is not null and int.TryParse is true
                {
                    return patientId; // return patient id
                }
            }
            return null;
        }
    }
}
