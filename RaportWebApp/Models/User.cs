using System;
using System.ComponentModel.DataAnnotations;

namespace RaportDB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Pamiętaj o hashowaniu!

        public DateTime BirthDate { get; set; }

        public bool isAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}