using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; } 

        [Required(ErrorMessage = "City Name is required.")]
        [MaxLength(100, ErrorMessage = "City Name cannot exceed 100 characters.")]
        public string CityName { get; set; }
        public State State { get; set; } 

        [ForeignKey("State")]
        [Required(ErrorMessage = "StateId is required.")]
        public Guid StateId { get; set; } 

        public ICollection<Customer>? Customers { get; set; } 
    }
}
