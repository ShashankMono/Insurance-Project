using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } // Employee's first name

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } // Employee's last name

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string MobileNo { get; set; } // Employee's mobile number

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; } // Employee's address

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailId { get; set; } // Employee's email address

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; } // Employee's salary

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; } // Link to User Entity
        public User User { get; set; }
    }
}
