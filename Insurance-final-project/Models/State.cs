using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class State
    {
        [Key]
        public Guid StateId { get; set; } 

        [Required(ErrorMessage = "State Name is required.")]
        [MaxLength(100, ErrorMessage = "State Name cannot exceed 100 characters.")]
        public string StateName { get; set; } 
        public ICollection<City> Cities { get; set; } 

        public ICollection<Customer>? Customer { get; set; }
    }
}
