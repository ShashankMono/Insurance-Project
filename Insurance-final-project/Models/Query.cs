using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Query
    {
        [Key]
        public Guid QueryId { get; set; } 

        [Required(ErrorMessage = "Question is required.")]
        [MaxLength(1000, ErrorMessage = "Question cannot exceed 1000 characters.")]
        public string Question { get; set; } 

        [MaxLength(1000, ErrorMessage = "Response cannot exceed 1000 characters.")]
        public string Response { get; set; } 

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } 

        public Customer Customer { get; set; } 
    }

}
