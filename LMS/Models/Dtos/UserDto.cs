using LMS.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.Dtos
{
    public class UserDto
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [MinLength(6)]
        public required string PasswordHash { get; set; }
    }
}
