using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Database
{
    public class UserDatabaseDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public Guid DatabaseId { get; set; }
        public string DatabaseName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class CreateUserDatabaseRequest
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public Guid DatabaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!; // "admin" veya "user"
    }

    public class UpdateUserDatabaseRequest
    {
        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!; // "admin" veya "user"

        [Required]
        public bool IsActive { get; set; }
    }
}
