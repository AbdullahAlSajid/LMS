using LMS.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.Dtos.UserDtos
{
    public class UpdateUserDto
    {
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
    }
}
