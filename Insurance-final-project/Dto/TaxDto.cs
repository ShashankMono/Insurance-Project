using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class TaxDto
    {
        public Guid TaxId { get; set; }

        [Required(ErrorMessage = "Tax percentage is required.")]
        public double TaxPercentage { get; set; }
    }
}
