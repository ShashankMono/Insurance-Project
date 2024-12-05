using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCity([FromBody] CityDto city)
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

            var cityId = await _cityService.AddCity(city);

            return Ok(new
            {
                Success = true,
                Data = cityId,
                Message = "City added successfully."
            });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCity([FromBody] CityDto city)
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

            var cityId = await _cityService.UpdateCity(city);

            return Ok(new
            {
                Success = true,
                Data = cityId,
                Message = "City updated successfully."
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _cityService.GetCities();

            return Ok(new
            {
                Success = true,
                Data = cities,
                Message = "Cities retrieved successfully."
            });
        }

        [HttpGet("State/{id}")]
        public async Task<IActionResult> GetCitiedByStateId(Guid id)
        {
            var cities = _cityService.GetCitiesByStateId(id);

            return Ok(new
            {
                Success = true,
                Data = cities,
                Message = "cities retrived successfully!"
            });
        }
    }
}
