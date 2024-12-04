using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class StateDto
    {
        public Guid StateId { get; set; }
        [Required(ErrorMessage ="The state name field is required")]
        public string StateName { get; set; }
    }
}
