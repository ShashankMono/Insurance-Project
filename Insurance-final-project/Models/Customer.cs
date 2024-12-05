using Insurance_final_project.Constant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Insurance_final_project.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } 

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number format.")]
        [MaxLength(10, ErrorMessage = "Mobile Number cannot exceed 10 characters.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DateOfBirth { get; set; }

        // Relationships with State and City
        [Required(ErrorMessage = "State ID is required.")]
        [ForeignKey("State")]
        public Guid StateId { get; set; }
        public State? State { get; set; } 

        [Required(ErrorMessage = "City ID is required.")]
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public City? City { get; set; } 

        // Nominee Details
        public ICollection<Nominee>? Nomines { get; set; } 

        public ICollection<PolicyAccount>? PolicyAccounts { get; set; }

        public ICollection<Document>? Documents { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Query>? Queries { get; set; }

        [Required(ErrorMessage = "Approval status is required.")]
        [MaxLength(20, ErrorMessage = "Approval status cannot exceed 20 characters.")]
        public string IsApproved { get; set; } = ApprovalType.Pending.ToString();

        public ICollection<Transaction>? Transactions { get; set; }
    }

}
