using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class QueryDto
    {

        public Guid QueryId { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [MaxLength(1000, ErrorMessage = "Question cannot exceed 1000 characters.")]
        public string Question { get; set; }

        [MaxLength(1000, ErrorMessage = "Response cannot exceed 1000 characters.")]
        public string Response { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid CustomerId { get; set; }

        public string? CustomerName { get; set; }
    }
}
