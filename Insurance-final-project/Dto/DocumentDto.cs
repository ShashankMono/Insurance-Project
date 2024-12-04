using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(100, ErrorMessage = "Document Type cannot be longer than 100 characters.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(200, ErrorMessage = "Document Name cannot be longer than 200 characters.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Document File is required.(Document not uploaded)")]
        public string DocumentFileURL { get; set; }  // You can use a string for file paths or URIs

        //Check verification of the document which will be changed by employee
        public bool IsVerified { get; set; } = false;

        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid CustomerId { get; set; }

    }
}
