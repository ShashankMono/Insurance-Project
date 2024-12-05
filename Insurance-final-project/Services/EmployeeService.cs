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
        public EmployeeService(
        IRepository<Employee> employeeRepo,
        IUserService userService,
        IRepository<Role> roleRepo,
        IMapper mapper)
        {
            _RoleRepo = roleRepo;
            _userService = userService;
            _EmployeeRepo = employeeRepo;
            _Mapper = mapper;
        }
        public async Task<UserDto> AddEmployee(EmployeeDto newEmployee)
        {
            Employee employee = _Mapper.Map<EmployeeDto, Employee>(newEmployee);
            UserDto user = _userService.AddNewUser(_RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Employee").RoleId);
            employee.UserId = user.UserId;
            Employee employeeAdded = _EmployeeRepo.Add(employee);
            return user;
        }

        public async Task<List<EmployeeDto>> GetAllEmployee()
        {
            return _Mapper.Map<List<Employee>, List<EmployeeDto>>(_EmployeeRepo.GetAll().ToList());
        }

        public async Task<Guid> UpdateEmployeeProfile(EmployeeDto employee)
        {
            if(_EmployeeRepo.GetAll().AsNoTracking().FirstOrDefault(e=>e.EmployeeId == employee.EmployeeId) == null)
            {
                throw new InvalidEmployeeException("Employee not found!");
            }
            return _EmployeeRepo.Update(_Mapper.Map<EmployeeDto, Employee>(employee)).EmployeeId;
        }
    }
}
