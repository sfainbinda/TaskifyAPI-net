﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [NotMapped]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string? RepeatPassword { get; set; }

        [NotMapped]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
    }
}
