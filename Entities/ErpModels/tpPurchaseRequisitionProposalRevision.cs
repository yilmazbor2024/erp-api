using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionProposalRevision")]
    public partial class tpPurchaseRequisitionProposalRevision
    {
        public tpPurchaseRequisitionProposalRevision()
        {
            rpPurchaseRequisitionProposalConfirmationHistorys = new HashSet<rpPurchaseRequisitionProposalConfirmationHistory>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionProposalID { get; set; }

        [Key]
        [Required]
        public byte RevisionNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RevisionDescription { get; set; }

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
        public virtual tpPurchaseRequisitionProposal tpPurchaseRequisitionProposal { get; set; }

        public virtual ICollection<rpPurchaseRequisitionProposalConfirmationHistory> rpPurchaseRequisitionProposalConfirmationHistorys { get; set; }
    }
}
