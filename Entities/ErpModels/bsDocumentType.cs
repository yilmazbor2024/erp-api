using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDocumentType")]
    public partial class bsDocumentType
    {
        public bsDocumentType()
        {
            bsDocumentTypeDescs = new HashSet<bsDocumentTypeDesc>();
            cdExpenseTypes = new HashSet<cdExpenseType>();
            cdPOSTerminals = new HashSet<cdPOSTerminal>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            trBankLines = new HashSet<trBankLine>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trJournalLines = new HashSet<trJournalLine>();
        }

        [Key]
        [Required]
        public byte DocumentTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDocumentTypeDesc> bsDocumentTypeDescs { get; set; }
        public virtual ICollection<cdExpenseType> cdExpenseTypes { get; set; }
        public virtual ICollection<cdPOSTerminal> cdPOSTerminals { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
    }
}
