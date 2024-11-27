using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class PolicyType
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }

        public ICollection<Policy> Policies { get; set; }
    }
}
