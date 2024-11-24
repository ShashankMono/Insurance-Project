using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class State
    {
        [Key]
        public Guid StateId { get; set; }

        public string StateName { get; set; }
    }
}
