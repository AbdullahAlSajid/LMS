using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [MinLength(6)]
        public required string PasswordHash { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
