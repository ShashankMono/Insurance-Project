using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class PolicyType
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Policy Type is required.")]
        [MaxLength(100, ErrorMessage = "Policy Type cannot exceed 100 characters.")]
        public string Type { get; set; } 

        public bool IsActive { get; set; } = true; 

        public ICollection<Policy>? Policies { get; set; }
    }
}
