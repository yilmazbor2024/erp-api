using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPurchaseRequisitionProposalConfirmationEMailNotification")]
    public partial class trPurchaseRequisitionProposalConfirmationEMailNotification
    {
        public trPurchaseRequisitionProposalConfirmationEMailNotification()
        {
            rpProposalLineConfirmationHistorys = new HashSet<rpProposalLineConfirmationHistory>();
            rpPurchaseRequisitionProposalConfirmationHistorys = new HashSet<rpPurchaseRequisitionProposalConfirmationHistory>();
            tpProposalLineConfirmations = new HashSet<tpProposalLineConfirmation>();
            tpPurchaseRequisitionProposalConfirmations = new HashSet<tpPurchaseRequisitionProposalConfirmation>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionProposalConfirmationEMailNotificationID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [Required]
        public DateTime EMailSendDate { get; set; }

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
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }

        public virtual ICollection<rpProposalLineConfirmationHistory> rpProposalLineConfirmationHistorys { get; set; }
        public virtual ICollection<rpPurchaseRequisitionProposalConfirmationHistory> rpPurchaseRequisitionProposalConfirmationHistorys { get; set; }
        public virtual ICollection<tpProposalLineConfirmation> tpProposalLineConfirmations { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposalConfirmation> tpPurchaseRequisitionProposalConfirmations { get; set; }
    }
}
