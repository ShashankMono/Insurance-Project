using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Insurance_final_project.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; } // Primary Key
        public string EmployeeFirstName { get; set; } 
        public string EmployeeLastName { get; set; } 
        public string MobileNo { get; set; } 
        public string EmpAddress { get; set; } 
        public string EmailId { get; set; } 
        public decimal Salary { get; set; } 
        public bool IsActive { get; set; } = true; 

        // Foreign Key to User
        public int UserId { get; set; } // Link to User Entity
        public User User { get; set; } // Navigation property for User

    }
}
