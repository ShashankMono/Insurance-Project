using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class StateDto
    {
        [Key]
        public Guid StateId { get; set; } 

        [Required(ErrorMessage = "State Name is required.")]
        [MaxLength(100, ErrorMessage = "State Name cannot exceed 100 characters.")]
        public string StateName { get; set; } 
    }
}
