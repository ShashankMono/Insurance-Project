﻿using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
