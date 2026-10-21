using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace electronic_gradebook.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Subject { get; set; } = string.Empty;
        [Range(2, 6)]
        [Column(TypeName = "decimal(3,2)")]
        public decimal Score { get; set; }
        public DateTime DateGiven { get; set; } = DateTime.Today;
        [Required]
        public string StudentId { get; set; } = string.Empty;
        public IdentityUser? Student { get; set; }
    }
}
