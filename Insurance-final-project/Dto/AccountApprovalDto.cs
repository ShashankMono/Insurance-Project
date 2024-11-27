using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class AccountApprovalDto
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
