using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class State
    {
        [Key]
        public Guid StateId { get; set; }

        public string StateName { get; set; }

        public ICollection<City> Cities { get; set; }// OTM relationship //new
        public ICollection<Customer>? Customer { get; set; } 
    }
}
