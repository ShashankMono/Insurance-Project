using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class UpdateDocumentDto
    {
        public Guid DocumentId { get; set; }
        [Required(ErrorMessage = "Document File URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string DocumentFileURL { get; set; }
    }
}
