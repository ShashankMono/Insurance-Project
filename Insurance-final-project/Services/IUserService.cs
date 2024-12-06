using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IUserService
    {
        public Task<Guid> AddUser(UserDto user);// add new user
        public Task<(string token, UserLogInResponseDto userData)> LogIn(UserLoginDto user);// make login
        public Task<bool> UpdateUsername(UserUpdateDto user);// just password can be changed and can change username too
        public Task<List<UserDto>> GetUsers();// to verify the username are unique
        public  Task<UserDto> GetUserById(Guid id);
        public  Task<bool> ChangePassword(ChangePasswordDto changePassword);
        public  Task<bool> DeactivateUser(ChangeUserStatusDto changeStatus);
        public UserDto AddNewUser(Guid roleId);
        public Task<List<UserDto>> GetUsersByRole(Guid RoleId);
    }
}
