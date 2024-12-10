using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _EmployeeRepo;
        private readonly IUserService _userService;
        private readonly IMapper _Mapper;
        private readonly IRepository<Role> _RoleRepo;
        private readonly IEmailService _emailService;
        public EmployeeService(
        IRepository<Employee> employeeRepo,
        IUserService userService,
        IRepository<Role> roleRepo,
        IEmailService emailService,
        IMapper mapper)
        {
            _RoleRepo = roleRepo;
            _userService = userService;
            _EmployeeRepo = employeeRepo;
            _emailService = emailService;
            _Mapper = mapper;
            _userService = userService;
        }
        public async Task<UserDto> AddEmployee(EmployeeDto newEmployee)
        {
            Employee employee = _Mapper.Map<EmployeeDto, Employee>(newEmployee);
            var Role = _RoleRepo.GetAll().FirstOrDefault(r => r.RoleName.ToLower() == "Employee".ToLower());
            if (Role.RoleId == null)
            {
                throw new RoleNotFoundException("Role not found! Please add role \"Employee\"");
            }
            UserDto user = _userService.AddNewUser(Role.RoleId);
            employee.UserId = user.UserId;
            Employee employeeAdded = _EmployeeRepo.Add(employee);
            _emailService.SendUserDetailthroughEmail(employeeAdded.EmailId,"Employee username and password",user);
            return user;
        }

        public async Task<List<EmployeeDto>> GetAllEmployee()
        {
            return _Mapper.Map<List<Employee>, List<EmployeeDto>>(_EmployeeRepo.GetAll().ToList());
        }

        public async Task<Guid> UpdateEmployeeProfile(EmployeeDto employee)
        {
            var existingEmployee = _EmployeeRepo.GetAll().AsNoTracking().FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if ( existingEmployee== null)
            {
                throw new InvalidEmployeeException("Employee not found!");
            }
            employee.UserId = existingEmployee.UserId;
            return _EmployeeRepo.Update(_Mapper.Map<EmployeeDto, Employee>(employee)).EmployeeId;
        }
    }
}
