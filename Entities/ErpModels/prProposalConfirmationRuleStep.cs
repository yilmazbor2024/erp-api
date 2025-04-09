using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProposalConfirmationRuleStep")]
    public partial class prProposalConfirmationRuleStep
    {
        public prProposalConfirmationRuleStep()
        {
            prProposalConfirmationRuleStepUsers = new HashSet<prProposalConfirmationRuleStepUser>();
            rpProposalLineConfirmationHistorys = new HashSet<rpProposalLineConfirmationHistory>();
            rpPurchaseRequisitionProposalConfirmationHistorys = new HashSet<rpPurchaseRequisitionProposalConfirmationHistory>();
            tpProposalLineConfirmations = new HashSet<tpProposalLineConfirmation>();
            tpPurchaseRequisitionProposalConfirmations = new HashSet<tpPurchaseRequisitionProposalConfirmation>();
            trPurchaseRequisitionProposalConfirmationEMailNotificationDetails = new HashSet<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail>();
        }

        [Key]
        [Required]
        public Guid ProposalConfirmationRuleID { get; set; }

        [Key]
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public int ExpireTimeForNextStep { get; set; }

        [Required]
        public bool ValidateProposalConfirmationLimit { get; set; }

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
        public virtual cdProposalConfirmationRule cdProposalConfirmationRule { get; set; }

        public virtual ICollection<prProposalConfirmationRuleStepUser> prProposalConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<rpProposalLineConfirmationHistory> rpProposalLineConfirmationHistorys { get; set; }
        public virtual ICollection<rpPurchaseRequisitionProposalConfirmationHistory> rpPurchaseRequisitionProposalConfirmationHistorys { get; set; }
        public virtual ICollection<tpProposalLineConfirmation> tpProposalLineConfirmations { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposalConfirmation> tpPurchaseRequisitionProposalConfirmations { get; set; }
        public virtual ICollection<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail> trPurchaseRequisitionProposalConfirmationEMailNotificationDetails { get; set; }
    }
}
