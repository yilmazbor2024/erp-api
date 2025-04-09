using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxExemption")]
    public partial class bsTaxExemption
    {
        public bsTaxExemption()
        {
            bsTaxExemptionDescs = new HashSet<bsTaxExemptionDesc>();
            prProcessInfos = new HashSet<prProcessInfo>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trJournalLines = new HashSet<trJournalLine>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
        }

        [Key]
        [Required]
        public short TaxExemptionCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TopicNum { get; set; }

        [Required]
        public bool UseForDeclaration { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTaxExemptionDesc> bsTaxExemptionDescs { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
    }
}
