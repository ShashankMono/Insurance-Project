using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class CityDto
    {
        public Guid CityId { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        [MaxLength(100, ErrorMessage = "City Name cannot exceed 100 characters.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "StateId is required.")]
        public Guid StateId { get; set; }

    }
}
