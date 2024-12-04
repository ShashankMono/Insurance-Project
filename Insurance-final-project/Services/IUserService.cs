using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IUserService
    {
        public Task<Guid> AddUser(UserDto user);// add new user
        public Task<(string token, User userData)> LogIn(UserLoginDto user);// make login
        public Task<bool> UpdateUser(UserDto user);// just password can be changed and can change username too
        public Task<List<UserDto>> GetUsers();// to verify the username are unique
        public  Task<UserDto> GetUserById(Guid id);
        public  Task<bool> ChangePassword(ChangePasswordDto changePassword);
        public  Task<bool> DeactivateUser(ChangeUserStatusDto changeStatus);
    }
}
