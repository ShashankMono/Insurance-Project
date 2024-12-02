using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IUserService
    {
        public Guid AddUser(UserDto user);// add new user
        public (string token, User userData) LogIn(UserLoginDto user);// make login
        public bool UpdateUser(UserLoginDto user);// just password can be changed and can change username too
        public List<UserDto> GetUsers();// to verify the username are unique
        public UserDto GetUserById(Guid id);
        public bool ChangePassword(ChangePasswordDto changePassword);
        public bool DeactivateUser(ChangeUserStatusDto changeStatus);
    }
}
