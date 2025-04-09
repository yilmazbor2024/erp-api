using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreTotalDiscountAuthority")]
    public partial class dfStoreTotalDiscountAuthority
    {
        public dfStoreTotalDiscountAuthority()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Key]
        [Required]
        public byte DiscountReasonCode { get; set; }

        [Required]
        public float TotalMaxDiscountRate { get; set; }

        [Required]
        public byte AuthorityMethod { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountPassword { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PhoneNumberForSMS { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ContactFirstLastName { get; set; }

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
        public virtual dfStoreDefault dfStoreDefault { get; set; }
        public virtual cdDiscountReason cdDiscountReason { get; set; }

    }
}
