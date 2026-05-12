using ClinicWeb.Models.DTOs;
using ClinicWeb.Services.Appointments;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ClinicWeb.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace ClinicWeb.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;

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
                    if (string.IsNullOrEmpty(request.FirstName) ||
                        string.IsNullOrEmpty(request.LastName) ||
                        string.IsNullOrEmpty(request.Email))
                    {
                        return BadRequest(new { message = "For guest bookings, firstName, lastName, and email are required." });
                    }

                    var guestPatient = new Patient
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Birthdate = request.Birthdate
                        // No password for guests
                    };

                    await _patientService.CreateAsync(guestPatient);
                    patientId = guestPatient.Id;
                }

                await _appointmentService.BookAsync(request, patientId);
                return CreatedAtAction(nameof(Book), new { message = "Appointment booked successfully." });
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
        /// Move an existing appointment to a new date and time. Only the patient who booked the appointment can move it, and they must be logged in to do so.
        /// </summary>
        /// <returns> Returns a confirmation message indicating successful rescheduling.</returns>

        [HttpPut("move")]
        [Authorize] // Only logged-in users can move appointments
        public async Task<IActionResult> Move([FromBody] MoveAppointmentDto request)
        {
            try
            {
                int? patientId = GetPatientId();
                if (patientId == null)
                    return Unauthorized(new { message = "You must be logged in to move an appointment." });

                await _appointmentService.MoveAsync(patientId.Value, request);
                return Ok(new { message = "Appointment moved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Cancel an appointment. Only the patient who booked the appointment can cancel it, and they must be logged in to do so.
        /// </summary>
        /// <returns> Returns a confirmation message indicating successful cancellation.</returns>
        
        [HttpPut("{id}/cancel")]
        [Authorize] // Only logged-in users can cancel appointments
        public async Task<IActionResult> Cancel(int id)
        {
            var patientId = GetPatientId();
            if (patientId == null)
                return Unauthorized(new { message = "You must be logged in to cancel an appointment." });

            var request = new CancelAppointmentDto { AppointmentId = id };

            try
            {
                await _appointmentService.CancelAsync(patientId.Value, request);
                return Ok(new { message = "Appointment cancelled successfully." });
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
        /// Get all appointments for the logged-in patient.
        /// </summary>
        /// <returns> Returns a list of appointments for the logged-in patient.</returns>
        
        [HttpGet("myappointments")]
        [Authorize]
        public async Task<IActionResult> GetMyAppointments()
        {
            var patientId = GetPatientId();
            if (patientId == null)
                return Unauthorized(new { message = "You must be logged in." });

            var appointments = await _appointmentService.GetPatientAppointmentsAsync(patientId.Value);
            return Ok(appointments);
        }

        private int? GetPatientId()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int patientId))
                {
                    return patientId;
                }
            }
            return null;
        }
    }
}
