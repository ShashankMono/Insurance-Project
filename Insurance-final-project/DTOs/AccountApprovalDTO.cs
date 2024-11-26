using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class AccountApprovalDTO
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
