using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }

        public string DocumentType { get; set; }

        public string DocumentName { get; set; }

        public string DocumentFileURL { get; set; }  // You can use a string for file paths or URIs

        //Check verification of the document which will be changed by employee
        public bool IsVerified    { get; set; } 

        // Foreign Key to Customer (many-to-one relationship)
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        // Navigation property to Customer
        public Customer? Customer { get; set; }
    }
}
