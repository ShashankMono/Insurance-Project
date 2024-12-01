using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IUserService
    {
        public Guid DeActivateUser(UserDto user);
        public Guid AddUser(UserDto user);
        public bool LogIn(UserDto user);
        public bool UpdateUser(UserDto user);
        public List<UserDto> GetUsers();
    }
}
