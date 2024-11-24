using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class User
    {
        [Key]

        public Guid UserId { get; set; }
        [Required,StringLength(25)]
        public string UserName { get; set; }
        [Required,StringLength(60)]
        public string UserPasswordhash {  get; set; }
        [Required]
        public string Role { get; set; }
    }

}
