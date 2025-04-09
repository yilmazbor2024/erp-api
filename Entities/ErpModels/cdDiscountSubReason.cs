using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountSubReason")]
    public partial class cdDiscountSubReason
    {
        public cdDiscountSubReason()
        {
            cdDiscountSubReasonDescs = new HashSet<cdDiscountSubReasonDesc>();
            prDiscountReasonSubReasons = new HashSet<prDiscountReasonSubReason>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
            tpOrderHeaderExtensions = new HashSet<tpOrderHeaderExtension>();
        }

        [Key]
        [Required]
        public byte DiscountSubReasonCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdDiscountSubReasonDesc> cdDiscountSubReasonDescs { get; set; }
        public virtual ICollection<prDiscountReasonSubReason> prDiscountReasonSubReasons { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
        public virtual ICollection<tpOrderHeaderExtension> tpOrderHeaderExtensions { get; set; }
    }
}
