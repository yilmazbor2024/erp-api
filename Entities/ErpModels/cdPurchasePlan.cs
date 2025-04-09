using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPurchasePlan")]
    public partial class cdPurchasePlan
    {
        public cdPurchasePlan()
        {
            cdPurchasePlanDescs = new HashSet<cdPurchasePlanDesc>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurchasePlanCode { get; set; }

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

        public virtual ICollection<cdPurchasePlanDesc> cdPurchasePlanDescs { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
