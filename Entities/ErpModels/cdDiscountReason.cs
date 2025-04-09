using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountReason")]
    public partial class cdDiscountReason
    {
        public cdDiscountReason()
        {
            cdDiscountReasonDescs = new HashSet<cdDiscountReasonDesc>();
            dfStoreTotalDiscountAuthoritys = new HashSet<dfStoreTotalDiscountAuthority>();
            prDiscountReasonSubReasons = new HashSet<prDiscountReasonSubReason>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
        }

        [Key]
        [Required]
        public byte DiscountReasonCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebSiteURL { get; set; }

        [Required]
        public bool IsCardPayment { get; set; }

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

        public virtual ICollection<cdDiscountReasonDesc> cdDiscountReasonDescs { get; set; }
        public virtual ICollection<dfStoreTotalDiscountAuthority> dfStoreTotalDiscountAuthoritys { get; set; }
        public virtual ICollection<prDiscountReasonSubReason> prDiscountReasonSubReasons { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
    }
}
