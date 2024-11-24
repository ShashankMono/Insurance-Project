using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }

        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(100, ErrorMessage = "Document Type cannot be longer than 100 characters.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(200, ErrorMessage = "Document Name cannot be longer than 200 characters.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Document File is required.")]
        public string DocumentFile { get; set; }  // You can use a string for file paths or URIs

        // Foreign Key to Customer (many-to-one relationship)
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        // Navigation property to Customer
        public Customer? Customer { get; set; }
    }
}
