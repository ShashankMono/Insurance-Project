﻿using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Employee Employee { get; set; }//nav
    }

}
