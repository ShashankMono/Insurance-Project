using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Policy
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("PolicyType")]
        public Guid PolicyTypeId { get; set; }
        public PolicyType PolicyType { get; set; }

        public int MinimumAgeCriteria { get; set; }
        public int MaximumAgeCriteria { get; set; }
        public decimal MinimumInvestmentAmount { get; set; }
        public int MinimumPolicyTerm { get; set; }
        public int MaximumPolicyTerm { get; set; }
        public decimal MaximumInvestmentAmount { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal CommissionPercentage { get; set; }

        public ICollection<PolicyAccount> PolicyAccounts { get; set; }
    }
}
