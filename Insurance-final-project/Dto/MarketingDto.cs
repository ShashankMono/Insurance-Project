using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class MarketingDto
    {
        [Required(ErrorMessage ="Customer is required")]
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "Agent name is required")]
        [MaxLength(100, ErrorMessage = "Agent Name cannot exceed 100 characters.")]
        public string AgentName { get; set; }
        [Required(ErrorMessage = "Policy name is required")]
        [MaxLength(100, ErrorMessage = "Policy Name cannot exceed 100 characters.")]
        public string PolicyName { get; set; }
        [Required(ErrorMessage = "Url is required")]
        [Url(ErrorMessage = "Invalid image URL format.")]
        public string Url { get; set; }

    }
}
