﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Withdrawal
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; } // Commission or Cancel Policy
        public decimal Amount { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
