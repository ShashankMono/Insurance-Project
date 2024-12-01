using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IEmployeeService
    {
        public Guid AddEmployee(EmployeeDto employee);
        public List<EmployeeDto> GetAllEmployee();
        public Guid UpdateEmployee(EmployeeDto employee);
        public Guid UpdateEmployeeStatus(string status);
        public EmployeeDto GetEmployeeById(Guid id);
        public EmployeeDto GetById(Guid id);
        public bool DeleteEmployee(Guid id);

    }
}
