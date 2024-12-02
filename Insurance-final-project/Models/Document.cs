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

        public string DocumentFileURL { get; set; } 

        //Check verification of the document which will be changed by employee
        public bool IsVerified { get; set; } = false;
        //public string IsVerified { get; set; } = VerificationType.Pending.ToString();
        // Foreign Key to Customer (many-to-one relationship)
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid CustomerId { get; set; }

        // Navigation property to Customer
        public Customer Customer { get; set; }
    }
}
