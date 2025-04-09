using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInvoiceType")]
    public partial class bsInvoiceType
    {
        public bsInvoiceType()
        {
            bsEInvoiceStatuss = new HashSet<bsEInvoiceStatus>();
            bsInvoiceTypeDescs = new HashSet<bsInvoiceTypeDesc>();
            prCurrAccEInvoiceOfficialForms = new HashSet<prCurrAccEInvoiceOfficialForm>();
            tpJournalLineExtensions = new HashSet<tpJournalLineExtension>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
        }

        [Key]
        [Required]
        public byte InvoiceTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEInvoiceStatus> bsEInvoiceStatuss { get; set; }
        public virtual ICollection<bsInvoiceTypeDesc> bsInvoiceTypeDescs { get; set; }
        public virtual ICollection<prCurrAccEInvoiceOfficialForm> prCurrAccEInvoiceOfficialForms { get; set; }
        public virtual ICollection<tpJournalLineExtension> tpJournalLineExtensions { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
    }
}
