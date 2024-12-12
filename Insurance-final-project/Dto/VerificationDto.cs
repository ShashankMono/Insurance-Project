using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class VerificationDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Approval status is required.")]
        [RegularExpression("^(Verified|Pending|Rejected)$", ErrorMessage = "Approval status must be 'Verified', 'Pending', or 'Rejected'.")]
        public string IsVerified { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid AccountId { get; set; }

        public string Reason { get; set; }
    }
}
