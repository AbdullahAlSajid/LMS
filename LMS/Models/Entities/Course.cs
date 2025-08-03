using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.Entities
{
    public enum CourseStatus
    {
        DRAFT,
        PUBLISHED,
        ARCHIVED
    }

    public class Course
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("tenant_id")]
        public long TenantId { get; set; }

        /*
        * TODO: Navigation Property need to add with Tenant 
        * public Tenant Tenant { get; set; }
        */

        [Column("code")]
        [StringLength(50)]
        public string? Code { get; set; }

        [Column("title")]
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; } = 0;

        [Column("currency")]
        [StringLength(3)]
        public string Currency { get; set; } = "USD";

        [Column("status")]
        public CourseStatus Status { get; set; } = CourseStatus.DRAFT;

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        public User? CreatedByUser { get; set; }    // Navigation property

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
