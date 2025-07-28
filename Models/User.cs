using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{

    public enum UserStatus
    {
        Active = 0,
        Invited = 1,
        Suspended = 2
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[ForeignKey("Tenant")]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
