using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Register([FromBody] UserDto newUser)
        {
            ValidateModel(); // Validate the model

            var user = _userService.AddUser(newUser);
            return Ok(new { Success = true, Data = user, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDetails)
        {
            ValidateModel(); // Validate the model

            var user = _userService.LogIn(loginDetails);
            return Ok(new { Success = true, Data = user, Message = "User logged in successfully." });
        }

        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] UserDto updatedUser)
        {
            ValidateModel(); // Validate the model

            var userId = _userService.UpdateUser(updatedUser);
            return Ok(new { Success = true, Data = userId, Message = "User updated successfully." });
        }

        [HttpGet("get-profile")]
        public IActionResult GetUserProfile([FromQuery] Guid userId)
        {
            var userProfile = _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = userProfile, Message = "User profile retrieved successfully." });
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            ValidateModel(); // Validate the model

            var isChanged = _userService.ChangePassword(changePasswordDto);
            return Ok(new { Success = true, Data = isChanged, Message = "Password changed successfully." });
        }


        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            return Ok(new { Success = true, Data = users, Message = "All users retrieved successfully." });
        }

        [HttpPost("deactivate")]
        public IActionResult DeactivateUser([FromBody] ChangeUserStatusDto userStatus)
        {
            ValidateModel(); // Validate the model

            var userId = _userService.DeactivateUser(userStatus);
            return Ok(new { Success = true, Data = userId, Message = "User deactivated successfully." });
        }

        [HttpGet("get-user")]
        public IActionResult GetUser([FromQuery] Guid userId)
        {
            var user = _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = user, Message = "User retrieved successfully." });
        }
    }
}
