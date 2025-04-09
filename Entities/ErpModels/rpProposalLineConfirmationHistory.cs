using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpProposalLineConfirmationHistory")]
    public partial class rpProposalLineConfirmationHistory
    {
        public rpProposalLineConfirmationHistory()
        {
        }

        [Key]
        [Required]
        public Guid ProposalLineID { get; set; }

        [Key]
        [Required]
        public byte RevisionNumber { get; set; }

        [Required]
        public Guid ProposalConfirmationRuleID { get; set; }

        [Key]
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RejectReason { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        public Guid? PurchaseRequisitionProposalConfirmationEMailNotificationID { get; set; }

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
        public virtual tpProposalLineRevision tpProposalLineRevision { get; set; }
        public virtual trPurchaseRequisitionProposalConfirmationEMailNotification trPurchaseRequisitionProposalConfirmationEMailNotification { get; set; }

    }
}
