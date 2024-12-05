using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IEmployeeService
    {
        Task<Guid> UpdateEmployeeProfile(EmployeeDto employee);
        Task<UserDto> AddEmployee(EmployeeDto employee);
        Task<List<EmployeeDto>> GetAllEmployee();

    }
}
