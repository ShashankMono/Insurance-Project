﻿using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}"), Authorize(Roles = "Customer,Employee,Admin")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);

            return Ok(new
            {
                Success = true,
                Data = customer,
                Message = "Customer retrieved successfully."
            });
        }

        [HttpGet("User/{UserId}"), Authorize(Roles = "Customer")]
        public IActionResult GetCustomerByUserId(Guid UserId)
        {
            var customer = _customerService.GetCustomerByUserId(UserId);

            return Ok(new
            {
                Success = true,
                Data = customer,
                Message = "Customer retrieved successfully."
            });
        }


        [HttpPut,Authorize(Roles ="Customer")]
        public IActionResult UpdateProfile([FromBody] CustomerDto customerDto)
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

            _customerService.UpdateProfile(customerDto);

            return Ok(new
            {
                Success = true,
                Data = (object)null,
                Message = "Customer profile updated successfully."
            });
        }

        [HttpPost("approve"), Authorize(Roles = "Employee")]
        public IActionResult ApproveCustomer([FromBody] ApprovalDto approval)
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

            var response = _customerService.ApproveCustomer(approval);

            return Ok(new
            {
                Success = true,
                Data = response,
                Message = "Customer profile updated successfully."
            });
        }

        [HttpPost]
        public IActionResult RegisterCustomer([FromBody] CustomerDto customerDto)
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

            var newCustomer = _customerService.RegisterCustomer(customerDto);

            return Ok(new
            {
                Success = true,
                Data = newCustomer,
                Message = "Customer registered successfully."
            });
        }

        [HttpGet,Authorize(Roles ="Admin,Employee,Agent")]
        public async Task<IActionResult> GetCustomerAccounts([FromQuery] PageParameters pageParameter,
            [FromQuery] string? searchQuery)
        {
            var customers = await _customerService.GetCustomerAccounts(searchQuery);

            var pagedData = PageList<CustomerProfileDto>.ToPagedList(customers, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
            {
                Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "Customers and their accounts retrieved successfully."
            });
        }
    }
}
