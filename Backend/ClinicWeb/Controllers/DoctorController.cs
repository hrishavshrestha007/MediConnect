using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService _doctorService)
        {
            this._doctorService = _doctorService;
        }

        /// <summary>
        /// Get all doctors.
        /// </summary>
        /// <returns> Returns a list of all doctors. 200 OK. </returns>

        //GET api/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            var doctorDtos = doctors.Select(doctor => new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"Dr. {doctor.FirstName} {doctor.LastName}",
                SpecialityName = doctor.Speciality?.Name,
                ClinicName = doctor.Clinic?.Name
            }).ToList();
            return Ok(doctorDtos);
        }

        /// <summary>
        /// Get a doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns the doctor with the specified ID. 200 OK. </returns>

        //GET api/doctors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                var doctorDto = new DoctorDto
                {
                    Id = doctor.Id,
                    FullName = $"Dr. {doctor.FirstName} {doctor.LastName}",
                    SpecialityName = doctor.Speciality?.Name,
                    ClinicName = doctor.Clinic?.Name
                };
                return Ok(doctorDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Search for doctors by name, speciality, or clinic. The search term is passed as a query parameter.
        /// </summary>
        /// <param name="term"></param>
        /// <returns> Returns a list of doctors matching the search criteria. 200 OK. </returns>

        //Get api/doctors/search?term={searchTerm}
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            try
            {
                var results = await _doctorService.SearchAsync(term);
                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new doctor.
        /// </summary>
        /// <param name="NewDoctor"></param>
        /// <returns> Returns the created doctor. 201 Created. </returns>

        //POST api/doctors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Doctor NewDoctor)
        {
            try
            {
                await _doctorService.CreateAsync(NewDoctor);
                var createdDoctor = await _doctorService.GetByIdAsync(NewDoctor.Id);
                return CreatedAtAction(nameof(GetById), new { id = NewDoctor.Id }, createdDoctor);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedDoctor"></param>
        /// <returns> Returns the updated doctor. 200 OK. </returns>

        //PUT api/doctors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Doctor updatedDoctor)
        {
            try
            {
                await _doctorService.UpdateAsync(id, updatedDoctor);
                var updateDoctor = await _doctorService.GetByIdAsync(id);
                return Ok(updateDoctor);
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
        /// Delete a doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns a confirmation message. 200 OK. </returns>

        //DELETE api/doctors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _doctorService.DeleteAsync(id);
                var deletedDoctor = new { message = $"Doctor with ID {id} has been deleted." };
                return Ok(deletedDoctor);
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
    }
}