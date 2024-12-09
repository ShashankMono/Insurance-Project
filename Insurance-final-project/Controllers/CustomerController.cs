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

        [HttpGet("{customerId}")]
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

        [HttpGet("User/{UserId}")]
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


        [HttpPut]
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

        [HttpPost("/approve")]
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

            _customerService.ApproveCustomer(approval);

            return Ok(new
            {
                Success = true,
                Data = (object)null,
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

        [HttpGet]
        public async Task<IActionResult> GetCustomerAccounts()
        {
            var customers = await _customerService.GetCustomerAccounts();

            return Ok(new
            {
                Success = true,
                Data = customers,
                Message = "Customers and their accounts retrieved successfully."
            });
        }
        [HttpGet("User/{UserId}")]
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
    }
}
