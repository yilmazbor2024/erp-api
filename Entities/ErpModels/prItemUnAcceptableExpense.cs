using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemUnAcceptableExpense")]
    public partial class prItemUnAcceptableExpense
    {
        public prItemUnAcceptableExpense()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte UnAcceptableExpenseTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UnAcceptableExpenseCode { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdItem cdItem { get; set; }

    }
}
