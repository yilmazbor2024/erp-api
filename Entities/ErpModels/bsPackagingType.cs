using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPackagingType")]
    public partial class bsPackagingType
    {
        public bsPackagingType()
        {
            bsPackagingTypeDescs = new HashSet<bsPackagingTypeDesc>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            trPickingHeaders = new HashSet<trPickingHeader>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackagingTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPackagingTypeDesc> bsPackagingTypeDescs { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
    }
}
