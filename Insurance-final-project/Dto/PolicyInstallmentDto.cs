using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Insurance_final_project.Dto
{
    public class PolicyInstallmentDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]

        public Guid PolicyAccountId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Start Date format.")]
        public DateTime StartDate { get; set; } 

        [Required(ErrorMessage = "Policy Term is required.")]
        [Range(1, 100, ErrorMessage = "Policy Term must be between 1 and 100 years.")]
        public int PolicyTerm { get; set; } 

        [Required(ErrorMessage = "Installment Type is required.")]
        [RegularExpression("^(Monthly|Quarterly|Yearly|HalfYearly)$", ErrorMessage = "Installment Type must be 'Monthly', 'Quarterly', or 'Yearly'.")]
        public string InstallmentType { get; set; } 

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }

    }
}
