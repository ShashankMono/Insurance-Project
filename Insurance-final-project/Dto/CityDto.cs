using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class CityDto
    {
        [Required(ErrorMessage ="The city name field name is required")]
        public string CityName { get; set; }

    }
}
