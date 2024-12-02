using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public State State { get; set; }
        [ForeignKey("State")]
        public Guid StateId { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
