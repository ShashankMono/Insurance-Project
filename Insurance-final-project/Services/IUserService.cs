using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IUserService
    {
        public Task<Guid> AddUser(UserDto user);
        public Task<(string token, UserLogInResponseDto userData)> LogIn(UserLoginDto user);
        public Task<bool> UpdateUsername(UserUpdateDto user);
        public Task<List<UserLogInResponseDto>> GetUsers(string? searchQuery);
        public  Task<UserDto> GetUserById(Guid id);
        public  Task<bool> ChangePassword(ChangePasswordDto changePassword);
        public  Task<bool> DeactivateUser(ChangeUserStatusDto changeStatus);
        public UserDto AddNewUser(Guid roleId);
        public Task<List<UserDto>> GetUsersByRole(Guid RoleId);
    }
}
