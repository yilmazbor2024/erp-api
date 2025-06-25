using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Database
{
    public class DatabaseDto
    {
        public Guid Id { get; set; }
        public string DatabaseName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? CompanyAddress { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyTaxNumber { get; set; }
        public string? CompanyTaxOffice { get; set; }
        public string ServerName { get; set; } = null!;
        public string? ServerPort { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class CreateDatabaseRequest
    {
        [Required]
        [StringLength(100)]
        public string DatabaseName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = null!;

        [StringLength(200)]
        public string? CompanyAddress { get; set; }

        [StringLength(20)]
        public string? CompanyPhone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? CompanyEmail { get; set; }

        [StringLength(20)]
        public string? CompanyTaxNumber { get; set; }

        [StringLength(100)]
        public string? CompanyTaxOffice { get; set; }

        [Required]
        [StringLength(500)]
        public string ConnectionString { get; set; } = null!;

        [StringLength(100)]
        public string? ServerName { get; set; }

        [StringLength(50)]
        public string? ServerPort { get; set; }
    }

    public class UpdateDatabaseRequest
    {
        [Required]
        [StringLength(100)]
        public string DatabaseName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = null!;

        [StringLength(200)]
        public string? CompanyAddress { get; set; }

        [StringLength(20)]
        public string? CompanyPhone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? CompanyEmail { get; set; }

        [StringLength(20)]
        public string? CompanyTaxNumber { get; set; }

        [StringLength(100)]
        public string? CompanyTaxOffice { get; set; }

        [Required]
        [StringLength(500)]
        public string ConnectionString { get; set; } = null!;

        [StringLength(100)]
        public string? ServerName { get; set; }

        [StringLength(50)]
        public string? ServerPort { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
