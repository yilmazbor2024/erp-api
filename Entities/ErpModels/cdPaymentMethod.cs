using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPaymentMethod")]
    public partial class cdPaymentMethod
    {
        public cdPaymentMethod()
        {
            cdExportFiles = new HashSet<cdExportFile>();
            cdImportFiles = new HashSet<cdImportFile>();
            cdPaymentMethodDescs = new HashSet<cdPaymentMethodDesc>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentMethodCode { get; set; }

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

        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<cdImportFile> cdImportFiles { get; set; }
        public virtual ICollection<cdPaymentMethodDesc> cdPaymentMethodDescs { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
    }
}
