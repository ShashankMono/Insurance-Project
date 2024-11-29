using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Insurance_final_project.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }

        // Relationships with State and City
        [ForeignKey("State")]
        public Guid StateId { get; set; }
        public State? State { get; set; }  // Navigation property

        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public City? City { get; set; }    // Navigation property

        // Nominee Details
        public string Nominee { get; set; }
        public string NomineeRelation { get; set; }

        // Relationships with Agent and Policy Account Many to many realtionship with customer
        public ICollection<Agent>? Agent { get; set; }  // Navigation property

        // Relationship with Policy Account (one-to-Many)
        public ICollection<PolicyAccount>? PolicyAccounts { get; set; }

        // Relationship with Documents (one-to-Many)
        public ICollection<Document>? Documents { get; set; }

        // Foreign key to User (one-to-one)
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Queries (one-to-many)
        public ICollection<Query>? Queries { get; set; }

        //Approval of cutomer account, admin will approve
        public bool IsApproved { get; set; } = false;

        //one-to-many realtionship with transaction table
        public ICollection<Transaction>? transactions { get; set; }
    }

}
