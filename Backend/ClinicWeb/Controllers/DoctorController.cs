using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.Entities;
using ClinicWeb.Services.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/doctors")] // route for doctors
    [ApiController] // api controller
    public class DoctorController : ControllerBase 
    {
        private readonly IDoctorService _doctorService; // service for doctors
        public DoctorController(IDoctorService _doctorService) // constructor for doctors
        {
            this._doctorService = _doctorService; // initialize doctor service
        }

        /// <summary>
        /// Get all doctors.
        /// </summary>
        /// <returns> Returns a list of all doctors. 200 OK. </returns>

        //GET api/doctors
        [HttpGet] // get all doctors
        public async Task<IActionResult> GetAll() // get all doctors
        {
            var doctors = await _doctorService.GetAllAsync(); // get all doctors from service
            var doctorDtos = doctors.Select(doctor => new DoctorDto // map doctor to doctor dto
            { // create doctor dto
                Id = doctor.Id,
                FullName = $"Dr. {doctor.FirstName} {doctor.LastName}",
                SpecialityName = doctor.Speciality?.Name,
                ClinicName = doctor.Clinic?.Name
            }).ToList(); // convert to list
            return Ok(doctorDtos); // return ok with doctor dtos
        }

        /// <summary>
        /// Get a doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns the doctor with the specified ID. 200 OK. </returns>

        //GET api/doctors/{id}
        [HttpGet("{id}")] // get doctor by id
        public async Task<IActionResult> GetById(int id) // get doctor by id
        {
            try // try to get doctor by id
            {
                var doctor = await _doctorService.GetByIdAsync(id); // get doctor by id from service
                var doctorDto = new DoctorDto // map doctor to doctor dto
                {
                    Id = doctor.Id,
                    FullName = $"Dr. {doctor.FirstName} {doctor.LastName}",
                    SpecialityName = doctor.Speciality?.Name,
                    ClinicName = doctor.Clinic?.Name
                };
                return Ok(doctorDto); // return ok with doctor dto
            }
            catch (KeyNotFoundException ex) // if doctor not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
        }

        /// <summary>
        /// Search for doctors by name, speciality, or clinic. The search term is passed as a query parameter.
        /// </summary>
        /// <param name="term"></param>
        /// <returns> Returns a list of doctors matching the search criteria. 200 OK. </returns>

        //Get api/doctors/search?term={searchTerm}
        [HttpGet("search")] // search for doctors by name, speciality, or clinic
        public async Task<IActionResult> Search([FromQuery] string term) // search for doctors by name, speciality, or clinic from query parameter
        {
            try
            {
                var results = await _doctorService.SearchAsync(term); // search for doctors by name from service
                return Ok(results); // return ok with search results
            }
            catch (ArgumentException ex) // if search term is invalid
            {
                return BadRequest(new { message = ex.Message }); // return bad request with message
            }
            catch (KeyNotFoundException ex) // if no doctors found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
        }

        /// <summary>
        /// Create a new doctor.
        /// </summary>
        /// <param name="NewDoctor"></param>
        /// <returns> Returns the created doctor. 201 Created. </returns>

        //POST api/doctors
        [HttpPost] // create new doctor
        public async Task<IActionResult> Create([FromBody] Doctor NewDoctor) // create new doctor from body
        {
            try
            {
                await _doctorService.CreateAsync(NewDoctor); // create doctor from service
                var createdDoctor = await _doctorService.GetByIdAsync(NewDoctor.Id); // get created doctor by id from service
                return CreatedAtAction(nameof(GetById), new { id = NewDoctor.Id }, createdDoctor); // return created at action with created doctor
            }
            catch (InvalidOperationException ex) // if doctor creation fails
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }

        /// <summary>
        /// Update an existing doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedDoctor"></param>
        /// <returns> Returns the updated doctor. 200 OK. </returns>

        //PUT api/doctors/{id}
        [HttpPut("{id}")] // update doctor by id
        public async Task<IActionResult> Update(int id, [FromBody] Doctor updatedDoctor) // update doctor by id from body
        {
            try
            {
                await _doctorService.UpdateAsync(id, updatedDoctor); // update doctor from service
                var updateDoctor = await _doctorService.GetByIdAsync(id); // get updated doctor by id from service
                return Ok(updateDoctor); // return ok with updated doctor
            }
            catch (KeyNotFoundException ex) // if doctor not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
            catch (InvalidOperationException ex) // if doctor update fails
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }

        /// <summary>
        /// Delete a doctor by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns a confirmation message. 200 OK. </returns>

        //DELETE api/doctors/{id}
        [HttpDelete("{id}")] // delete doctor by id
        public async Task<IActionResult> Delete(int id) // delete doctor by id
        {
            try
            {
                await _doctorService.DeleteAsync(id); // delete doctor from service
                var deletedDoctor = new { message = $"Doctor with ID {id} has been deleted." }; // create confirmation message
                return Ok(deletedDoctor); // return ok with confirmation message
            }
            catch (KeyNotFoundException ex) // if doctor not found
            {
                return NotFound(new { message = ex.Message }); // return not found with message
            }
            catch (InvalidOperationException ex) // if doctor deletion fails
            {
                return Conflict(new { message = ex.Message }); // return conflict with message
            }
        }
    }
}