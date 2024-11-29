namespace Insurance_final_project.Dto
{
    using Insurance_final_project.Models;
    using System.ComponentModel.DataAnnotations;
    using Document = Models.Document;

    public class CustomerDto
    {

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Phone(ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        // Foreign Key to State
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }

        // Foreign Key to City
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }

        [StringLength(100, ErrorMessage = "Nominee name cannot be longer than 100 characters.")]
        public string Nominee { get; set; }

        [StringLength(100, ErrorMessage = "Nominee relation cannot be longer than 100 characters.")]
        public string NomineeRelation { get; set; }


        // Foreign Key to User (One-to-One)
        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }
    }

}
