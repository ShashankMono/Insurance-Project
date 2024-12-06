using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class NomineeDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nominee Name is required.")]
        [MaxLength(100, ErrorMessage = "Nominee Name cannot exceed 100 characters.")]
        public string NomineeName { get; set; }

        [Required(ErrorMessage = "Nominee Relation is required.")]
        [MaxLength(50, ErrorMessage = "Nominee Relation cannot exceed 50 characters.")]
        public string NomineeRelation { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid CustomerId { get; set; }
    }
}
