using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdITAttribute")]
    public partial class cdITAttribute
    {
        public cdITAttribute()
        {
            cdITAttributeDescs = new HashSet<cdITAttributeDesc>();
            prInnerProcessITAttributes = new HashSet<prInnerProcessITAttribute>();
            prProcessITAttributes = new HashSet<prProcessITAttribute>();
            tpAllocationITAttributes = new HashSet<tpAllocationITAttribute>();
            tpContractITAttributes = new HashSet<tpContractITAttribute>();
            tpInnerITAttributes = new HashSet<tpInnerITAttribute>();
            tpInnerOrderITAttributes = new HashSet<tpInnerOrderITAttribute>();
            tpInvoiceITAttributes = new HashSet<tpInvoiceITAttribute>();
            tpOrderITAttributes = new HashSet<tpOrderITAttribute>();
            tpProposalITAttributes = new HashSet<tpProposalITAttribute>();
            tpShipmentITAttributes = new HashSet<tpShipmentITAttribute>();
            tpStockITAttributes = new HashSet<tpStockITAttribute>();
            tpTransferPlanITAttributes = new HashSet<tpTransferPlanITAttribute>();
        }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual cdITAttributeType cdITAttributeType { get; set; }

        public virtual ICollection<cdITAttributeDesc> cdITAttributeDescs { get; set; }
        public virtual ICollection<prInnerProcessITAttribute> prInnerProcessITAttributes { get; set; }
        public virtual ICollection<prProcessITAttribute> prProcessITAttributes { get; set; }
        public virtual ICollection<tpAllocationITAttribute> tpAllocationITAttributes { get; set; }
        public virtual ICollection<tpContractITAttribute> tpContractITAttributes { get; set; }
        public virtual ICollection<tpInnerITAttribute> tpInnerITAttributes { get; set; }
        public virtual ICollection<tpInnerOrderITAttribute> tpInnerOrderITAttributes { get; set; }
        public virtual ICollection<tpInvoiceITAttribute> tpInvoiceITAttributes { get; set; }
        public virtual ICollection<tpOrderITAttribute> tpOrderITAttributes { get; set; }
        public virtual ICollection<tpProposalITAttribute> tpProposalITAttributes { get; set; }
        public virtual ICollection<tpShipmentITAttribute> tpShipmentITAttributes { get; set; }
        public virtual ICollection<tpStockITAttribute> tpStockITAttributes { get; set; }
        public virtual ICollection<tpTransferPlanITAttribute> tpTransferPlanITAttributes { get; set; }
    }
}
