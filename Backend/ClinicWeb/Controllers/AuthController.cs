using System;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.DTOs.Auth;
using ClinicWeb.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/auth")] // route for auth
    [ApiController] // api controller
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService; // service for auth
        public AuthController(IAuthService authService) // constructor for auth
        {
            _authService = authService; // initialize auth service
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <returns> A JWT token if the login is successful, or 401 if the login fails.</returns>

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request) // login user
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password)) // if email or password is empty
            {
                return BadRequest(new { error = "Username and password are required." }); // return bad request
            }

            var isValidUser = await _authService.ValidateUserAsync( // validate user
                request.Email,
                request.Password
            );

            if (!isValidUser) // if user is not valid
            {
                return Problem( // return problem
                    title: "Authentication failed",
                    detail: "The username or password is incorrect.",
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }

            var user = await _authService.GetUserByUsernameAsync(request.Email); // get user by email

            if (user == null) // if user is not found
            {
                return Problem( 
                    title: "Authentication failed",
                    detail: "The username or password is incorrect.",
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }
            var token = _authService.GenerateToken(user); // generate token

            return Ok(new AuthResponseDto // return auth response dto
            {
                Token = token, // token
                Email = user.Email, // email
                FullName = $"{user.FirstName} {user.LastName}", // full name
                PatientId = user.Id // patient id
            });
        }

        /// <summary>
        /// Logout a user
        /// </summary>
        /// <returns> A confirmation message if the logout is successful, or 401 if the logout fails.</returns>

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() // logout user
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token)) // if token is empty
            {
                return Unauthorized(new { error = "No token provided." }); // return unauthorized
            }

            return Ok(new { message = "Logged out successfully." }); // return ok
        }
    }
}
