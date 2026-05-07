using System;
using ClinicWeb.Models.DTOs;
using ClinicWeb.Models.DTOs.Auth;
using ClinicWeb.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <returns> A JWT token if the login is successful, or 401 if the login fails.</returns>

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            var isValidUser = await _authService.ValidateUserAsync(
                request.Email,
                request.Password
            );

            if (!isValidUser)
            {
                return Problem(
                    title: "Authentication failed",
                    detail: "The username or password is incorrect.",
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }

            var user = await _authService.GetUserByUsernameAsync(request.Email);

            if (user == null)
            {
                return Problem(
                    title: "Authentication failed",
                    detail: "The username or password is incorrect.",
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }
            var token = _authService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}"
            });
        }

        /// <summary>
        /// Logout a user
        /// </summary>
        /// <returns> A confirmation message if the logout is successful, or 401 if the logout fails.</returns>

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { error = "No token provided." });
            }

            return Ok(new { message = "Logged out successfully." });
        }
    }
}
