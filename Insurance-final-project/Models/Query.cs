using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Query
    {
        [Key]
        public Guid QueryId { get; set; } // Primary Key
        public string Question { get; set; } // Query or question from the customer
        public string Response { get; set; } // Response to the customer's query

        // Navigation property to Customer (many-to-one relationship)
        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } // Foreign Key to Customer
    }

}
