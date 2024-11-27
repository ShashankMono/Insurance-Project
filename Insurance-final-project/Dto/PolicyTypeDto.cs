using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyTypeDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }
    }
}
