using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPut("update-username"), Authorize(Roles = "Admin,Agent,Employee,Customer")]
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

        [HttpGet("{userId}"), Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUserProfile( Guid userId)
        {
            var userProfile = await _userService.GetUserById(userId);
            return Ok(new { Success = true, Data = userProfile, Message = "User profile retrieved successfully." });
        }

        [HttpPut("change-password"), Authorize(Roles="Admin,Agent,Employee,Customer")]
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


        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PageParameters pageParameter,
            [FromQuery] string? searchQuery)
        {
            var users = await _userService.GetUsers(searchQuery);
            var pagedData = PageList<UserLogInResponseDto>.ToPagedList(users, pageParameter.PageNumber, pageParameter.PageSize);
            return Ok(new { Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "All users retrieved successfully." });
        }

        [HttpPost("deactivate"), Authorize(Roles="Admin")]
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

        [HttpGet("role/{RoleId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByRole( Guid RoleId)
        {
            var user = await _userService.GetUsersByRole(RoleId);
            return Ok(new { Success = true, Data = user, Message = "User retrieved successfully." });
        }
    }
}
