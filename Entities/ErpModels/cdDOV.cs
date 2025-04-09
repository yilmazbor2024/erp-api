using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDOV")]
    public partial class cdDOV
    {
        public cdDOV()
        {
            cdDOVDescs = new HashSet<cdDOVDesc>();
            prDOVGLAccss = new HashSet<prDOVGLAccs>();
            prProcessInfos = new HashSet<prProcessInfo>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trProposalLines = new HashSet<trProposalLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [Required]
        public byte DOVRate1 { get; set; }

        [Required]
        public byte DOVRate2 { get; set; }

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

        public virtual ICollection<cdDOVDesc> cdDOVDescs { get; set; }
        public virtual ICollection<prDOVGLAccs> prDOVGLAccss { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
    }
}
