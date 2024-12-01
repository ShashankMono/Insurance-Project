using Insurance_final_project.Dto;
using Insurance_final_project.Services;
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

        [HttpPost("register")]
        public IActionResult RegisterCustomer(CustomerDto customerDto)
        {
            var customer = _customerService.RegisterCustomer(customerDto);
            return Ok(customer);
        }

        [HttpGet("profile/{id}")]
        public IActionResult GetCustomerById(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPut("updateProfile")]
        public IActionResult UpdateProfile(CustomerDto customerDto)
        {
            var result = _customerService.UpdateProfile(customerDto);
            if (!result) 
                return BadRequest("Unable to update profile.");
            return Ok("Profile updated successfully.");
        }

        [HttpPost("submitQuery")]
        public IActionResult SubmitQuery(QueryDto queryDto)
        {
            _customerService.SubmitQuery(queryDto);
            return Ok("Query submitted successfully.");
        }

        [HttpGet("policies/{customerId}")]
        public IActionResult GetPoliciesByCustomer(Guid customerId)
        {
            var policies = _customerService.GetPoliciesByCustomer(customerId);
            return Ok(policies);
        }

        [HttpPost("cancelPolicy/{policyAccountId}")]
        public IActionResult CancelPolicy(Guid policyAccountId)
        {
            var success = _customerService.CancelPolicy(policyAccountId);
            if (!success) 
                return BadRequest("Unable to cancel policy.");
            return Ok("Policy cancelled successfully.");
        }

        [HttpPost("claimPolicy/{policyAccountId}")]
        public IActionResult ClaimPolicy(Guid policyAccountId,ClaimDto claimDto)
        {
            var success = _customerService.ClaimPolicy(policyAccountId, claimDto);
            if (!success) 
                return BadRequest("Unable to submit claim.");
            return Ok("Claim submitted successfully.");
        }

        [HttpGet("availablePolicies")]
        public IActionResult GetAvailablePolicies()
        {
            var policies = _customerService.GetAvailablePolicies();
            return Ok(policies);
        }
    }
}
