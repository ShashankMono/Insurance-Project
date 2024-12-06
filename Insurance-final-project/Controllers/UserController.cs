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
        private readonly IUserService _userService; 

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var user = await _userService.AddUser(newUser);
            return Ok(new { Success = true, Data = user, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDetails)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            (string token, UserLogInResponseDto userData) = await _userService.LogIn(loginDetails);
            var dataObj = new {token=token, userData=userData};
            Response.Headers.Add("Jwt", dataObj.token);
            return Ok(new { Success = true, Data = dataObj.userData, Message = "User logged in successfully." });
        }

        [HttpPut("UpdateUsernam")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var userId = await _userService.UpdateUsername(updatedUser);
            return Ok(new { Success = true, Data = userId, Message = "User updated successfully." });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile( Guid userId)
        {
            var userProfile = await _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = userProfile, Message = "User profile retrieved successfully." });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var isChanged = await _userService.ChangePassword(changePasswordDto);
            return Ok(new { Success = true, Data = isChanged, Message = "Password changed successfully." });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(new { Success = true, Data = users, Message = "All users retrieved successfully." });
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] ChangeUserStatusDto userStatus)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var userId = await _userService.DeactivateUser(userStatus);
            return Ok(new { Success = true, Data = userId, Message = "User deactivated successfully." });
        }

        [HttpGet("role/{RoleId}")]
        public async Task<IActionResult> GetUsersByRole( Guid RoleId)
        {
            var user = await _userService.GetUsersByRole(RoleId);
            return Ok(new { Success = true, Data = user, Message = "User retrieved successfully." });
        }
    }
}
