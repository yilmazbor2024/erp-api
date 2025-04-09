using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDiscountReasonSubReason")]
    public partial class prDiscountReasonSubReason
    {
        public prDiscountReasonSubReason()
        {
        }

        [Key]
        [Required]
        public byte DiscountReasonCode { get; set; }

        [Key]
        [Required]
        public byte DiscountSubReasonCode { get; set; }

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
        public virtual cdDiscountReason cdDiscountReason { get; set; }
        public virtual cdDiscountSubReason cdDiscountSubReason { get; set; }

    }
}
