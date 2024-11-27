using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class AccountApprovalDTO
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
