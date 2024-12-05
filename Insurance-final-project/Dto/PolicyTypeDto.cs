using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyTypeDto
    {
        [Key]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Policy Type is required.")]
        [MaxLength(100, ErrorMessage = "Policy Type cannot exceed 100 characters.")]
        public string Type { get; set; } 

        public bool IsActive { get; set; } = true; 
    }
}
