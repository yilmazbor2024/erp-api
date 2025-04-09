using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTransType")]
    public partial class bsTransType
    {
        public bsTransType()
        {
            bsInnerProcesss = new HashSet<bsInnerProcess>();
            bsProcesss = new HashSet<bsProcess>();
            bsTransTypeDescs = new HashSet<bsTransTypeDesc>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trReportedSaleHeaders = new HashSet<trReportedSaleHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trStocks = new HashSet<trStock>();
        }

        [Key]
        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsInnerProcess> bsInnerProcesss { get; set; }
        public virtual ICollection<bsProcess> bsProcesss { get; set; }
        public virtual ICollection<bsTransTypeDesc> bsTransTypeDescs { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trReportedSaleHeader> trReportedSaleHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
    }
}
