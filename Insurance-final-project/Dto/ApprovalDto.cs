using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class ApprovalDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Approval status is required.")]
        [RegularExpression("^(Approved|Pending|Rejected)$", ErrorMessage = "Approval status must be 'Approved', 'Pending', or 'Rejected'.")]
        public string IsApproved { get; set; }
    }
}
