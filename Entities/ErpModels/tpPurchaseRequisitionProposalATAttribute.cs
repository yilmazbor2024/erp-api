using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionProposalATAttribute")]
    public partial class tpPurchaseRequisitionProposalATAttribute
    {
        public tpPurchaseRequisitionProposalATAttribute()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionProposalID { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

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

        // Navigation Properties
        public virtual cdATAttribute cdATAttribute { get; set; }
        public virtual tpPurchaseRequisitionProposal tpPurchaseRequisitionProposal { get; set; }

    }
}
