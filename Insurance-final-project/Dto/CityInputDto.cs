using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class CityInputDto
    {
        public Guid CityId { get; set; }
        [Required(ErrorMessage = "The city name field name is required")]
        public string CityName { get; set; }
        public Guid StateId { get; set; }
    }
}
