using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; // Assuming IUserService is the interface for your service

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Helper function to validate model state
        private void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid model state.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            ValidateModel(); // Validate the model

            var user = await _userService.AddUser(newUser);
            return Ok(new { Success = true, Data = user, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDetails)
        {
            ValidateModel(); // Validate the model

            (string token, User userData) = await _userService.LogIn(loginDetails);
            var dataObj = new {token=token, userData=userData};
            return Ok(new { Success = true, Data = dataObj, Message = "User logged in successfully." });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser)
        {
            ValidateModel(); // Validate the model

            var userId = await _userService.UpdateUser(updatedUser);
            return Ok(new { Success = true, Data = userId, Message = "User updated successfully." });
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetUserProfile([FromQuery] Guid userId)
        {
            var userProfile = await _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = userProfile, Message = "User profile retrieved successfully." });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            ValidateModel(); // Validate the model

            var isChanged = await _userService.ChangePassword(changePasswordDto);
            return Ok(new { Success = true, Data = isChanged, Message = "Password changed successfully." });
        }


        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(new { Success = true, Data = users, Message = "All users retrieved successfully." });
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] ChangeUserStatusDto userStatus)
        {
            ValidateModel(); // Validate the model

            var userId = await _userService.DeactivateUser(userStatus);
            return Ok(new { Success = true, Data = userId, Message = "User deactivated successfully." });
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser([FromQuery] Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = user, Message = "User retrieved successfully." });
        }
    }
}
