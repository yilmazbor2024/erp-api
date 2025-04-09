using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPurchaseRequisitionProposalConfirmationEMailNotificationDetail")]
    public partial class trPurchaseRequisitionProposalConfirmationEMailNotificationDetail
    {
        public trPurchaseRequisitionProposalConfirmationEMailNotificationDetail()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionProposalConfirmationEMailNotificationDetailID { get; set; }

        [Required]
        public Guid PurchaseRequisitionProposalConfirmationEMailNotificationID { get; set; }

        public Guid? ProposalLineID { get; set; }

        public Guid? PurchaseRequisitionProposalID { get; set; }

        [Required]
        public Guid ProposalConfirmationRuleID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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
        public virtual prProposalConfirmationRuleStep prProposalConfirmationRuleStep { get; set; }
        public virtual trProposalLine trProposalLine { get; set; }
        public virtual tpPurchaseRequisitionProposal tpPurchaseRequisitionProposal { get; set; }

    }
}
