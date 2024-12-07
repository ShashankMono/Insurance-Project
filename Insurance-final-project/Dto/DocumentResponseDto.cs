using Insurance_final_project.Constant;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class DocumentResponseDto
    {
        public Guid DocumentId { get; set; }

        [Required(ErrorMessage = "Document Type is required.")]
        [MaxLength(50, ErrorMessage = "Document Type cannot exceed 50 characters.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [MaxLength(100, ErrorMessage = "Document Name cannot exceed 100 characters.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Document File URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string DocumentFileURL { get; set; }

        [Required(ErrorMessage = "Verification status is required.")]
        [MaxLength(20, ErrorMessage = "Verification status cannot exceed 20 characters.")]
        public string IsVerified { get; set; } = VerificationType.Pending.ToString();

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
    }
}
