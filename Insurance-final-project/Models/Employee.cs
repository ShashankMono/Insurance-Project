using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Insurance_final_project.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int MobileNo { get; set; }
        public int Salary { get; set; }
        public User User { get; set; } // Nav
        [ForeignKey("User")]
        public Guid UserId { get; set; }

    }
}
