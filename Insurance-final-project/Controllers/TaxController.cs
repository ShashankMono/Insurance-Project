using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTax([FromBody] TaxDto Tax)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var tax = await _taxService.AddTax(Tax.TaxPercentage);

            return Ok(new
            {
                Success = true,
                Data = tax,
                Message = "Tax added successfully."
            });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTax([FromBody] TaxDto Tax)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var tax = await _taxService.UpdateTax(Tax);

            return Ok(new
            {
                Success = true,
                Data = tax,
                Message = "Tax updated successfully."
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetTax()
        {

            var tax = await _taxService.GetTax();

            return Ok(new
            {
                Success = true,
                Data = tax,
                Message = "Tax retrived successfully."
            });
        }

    }
}
