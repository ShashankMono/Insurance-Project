using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        public string CityName { get; set; }
        public State State { get; set; }// MTO relationship // new
        public Guid StateId { get; set; }//new

        public ICollection<Customer>? Customers { get; set; }
    }
}
