using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PaymentDto
    {
        [Required(ErrorMessage = "Policy Scheme required!")]
        [StringLength(100, ErrorMessage = "Policy Name cannot exceed 100 characters.")]
        public string PolicyName { get; set; }

        [Required(ErrorMessage = "Amount required!")]
        [Range(1, long.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public long Amount { get; set; }

        [Required(ErrorMessage = "SuccessUrl required!")]
        [Url(ErrorMessage = "Invalid URL format for SuccessUrl.")]
        public string SuccessUrl { get; set; }

        [Required(ErrorMessage = "CancelUrl required!")]
        [Url(ErrorMessage = "Invalid URL format for CancelUrl.")]
        public string CancelUrl { get; set; }
    }
}
