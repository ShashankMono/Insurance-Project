using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IEmailService
    {
        void SendUserDetailthroughEmail(string toEmail, string subject,UserDto userDets);
    }
}
