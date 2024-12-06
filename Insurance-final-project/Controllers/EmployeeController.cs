using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto newEmployee)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var user = await _employeeService.AddEmployee(newEmployee);

            return Ok(new
            {
                Success = true,
                Data = user,
                Message = "Employee added successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployee();

            return Ok(new
            {
                Success = true,
                Data = employees,
                Message = "Employees retrieved successfully."
            });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeProfile([FromBody] EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var employeeId = await _employeeService.UpdateEmployeeProfile(employee);

            return Ok(new
            {
                Success = true,
                Data = employeeId,
                Message = "Employee profile updated successfully."
            });
        }
    }
}
