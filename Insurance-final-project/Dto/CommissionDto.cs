using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class CommissionDto
    {
        // no need of Dto
        [Required(ErrorMessage = "Commission type is required.")]
        [StringLength(100, ErrorMessage = "Commission type cannot exceed 100 characters.")]
        public string CommissionType { get; set; } 

        [Required(ErrorMessage = "Agent ID is required.")]
        [ForeignKey("Agent")]
        public Guid AgentId { get; set; } 

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; } 

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        //[CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidatePastDate))]
        public DateTime Date { get; set; } 
    }
}
