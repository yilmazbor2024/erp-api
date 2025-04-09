using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdVat")]
    public partial class cdVat
    {
        public cdVat()
        {
            cdVatDescs = new HashSet<cdVatDesc>();
            prItemTaxGrAtts = new HashSet<prItemTaxGrAtt>();
            prVatGLAccss = new HashSet<prVatGLAccs>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trSupportRequestLines = new HashSet<trSupportRequestLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VatCode { get; set; }

        [Required]
        public float VatRate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string POSSectionCode { get; set; }

        [Required]
        public int OfficialTaxTypeCode { get; set; }

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

        // Navigation Properties
        public virtual bsOfficialTaxType bsOfficialTaxType { get; set; }

        public virtual ICollection<cdVatDesc> cdVatDescs { get; set; }
        public virtual ICollection<prItemTaxGrAtt> prItemTaxGrAtts { get; set; }
        public virtual ICollection<prVatGLAccs> prVatGLAccss { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trSupportRequestLine> trSupportRequestLines { get; set; }
    }
}
