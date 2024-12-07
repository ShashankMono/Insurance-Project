using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class GetFileDto
    {
        [Required(ErrorMessage ="File is required")]
        public IFormFile file {  get; set; }
    }
}
