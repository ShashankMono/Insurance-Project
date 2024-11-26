using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class Query
    {

        [Required(ErrorMessage = "Question is required.")]
        [StringLength(500, ErrorMessage = "Question cannot exceed 500 characters.")]
        public string Question { get; set; }

        [StringLength(1000, ErrorMessage = "Response cannot exceed 1000 characters.")]
        public string Response { get; set; }

        // Navigation property to Customer (many-to-one relationship)
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }
    }
}
