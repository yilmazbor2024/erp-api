using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWithHoldingTaxType")]
    public partial class bsWithHoldingTaxType
    {
        public bsWithHoldingTaxType()
        {
            prWithHoldingTaxAvailableDovRatess = new HashSet<prWithHoldingTaxAvailableDovRates>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trJournalLines = new HashSet<trJournalLine>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trProposalLines = new HashSet<trProposalLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prWithHoldingTaxAvailableDovRates> prWithHoldingTaxAvailableDovRatess { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
    }
}
