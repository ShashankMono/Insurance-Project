using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Commission
    {
        [Key]
        public Guid CommissionId { get; set; }
        public string CommissionType { get; set; }
        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
        public double Amount { get; set; }
        public DateTime Date {  get; set; }
    }
}
