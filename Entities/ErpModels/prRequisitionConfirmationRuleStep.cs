using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prRequisitionConfirmationRuleStep")]
    public partial class prRequisitionConfirmationRuleStep
    {
        public prRequisitionConfirmationRuleStep()
        {
            prRequisitionConfirmationRuleStepUsers = new HashSet<prRequisitionConfirmationRuleStepUser>();
            rpPurchaseRequisitionConfirmationHistorys = new HashSet<rpPurchaseRequisitionConfirmationHistory>();
            tpPurchaseRequisitionConfirmations = new HashSet<tpPurchaseRequisitionConfirmation>();
            trPurchaseRequisitionConfirmationEMailNotifications = new HashSet<trPurchaseRequisitionConfirmationEMailNotification>();
        }

        [Key]
        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

        [Key]
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public int ExpireTimeForNextStep { get; set; }

        [Required]
        public bool ValidateRequisitionConfirmationLimit { get; set; }

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
        public virtual cdRequisitionConfirmationRule cdRequisitionConfirmationRule { get; set; }

        public virtual ICollection<prRequisitionConfirmationRuleStepUser> prRequisitionConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<rpPurchaseRequisitionConfirmationHistory> rpPurchaseRequisitionConfirmationHistorys { get; set; }
        public virtual ICollection<tpPurchaseRequisitionConfirmation> tpPurchaseRequisitionConfirmations { get; set; }
        public virtual ICollection<trPurchaseRequisitionConfirmationEMailNotification> trPurchaseRequisitionConfirmationEMailNotifications { get; set; }
    }
}
