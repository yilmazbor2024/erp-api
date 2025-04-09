using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPCT")]
    public partial class cdPCT
    {
        public cdPCT()
        {
            cdPCTDescs = new HashSet<cdPCTDesc>();
            prItemTaxGrAtts = new HashSet<prItemTaxGrAtt>();
            prPCTGLAccss = new HashSet<prPCTGLAccs>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PCTCode { get; set; }

        [Required]
        public float PCTRate { get; set; }

        [Required]
        public bool AddVat { get; set; }

        [Required]
        public bool IsUnitBase { get; set; }

        [Required]
        public decimal PCTAmount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

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
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }

        public virtual ICollection<cdPCTDesc> cdPCTDescs { get; set; }
        public virtual ICollection<prItemTaxGrAtt> prItemTaxGrAtts { get; set; }
        public virtual ICollection<prPCTGLAccs> prPCTGLAccss { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
    }
}
