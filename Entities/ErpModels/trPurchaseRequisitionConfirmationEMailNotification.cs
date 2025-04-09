using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPurchaseRequisitionConfirmationEMailNotification")]
    public partial class trPurchaseRequisitionConfirmationEMailNotification
    {
        public trPurchaseRequisitionConfirmationEMailNotification()
        {
            rpPurchaseRequisitionConfirmationHistorys = new HashSet<rpPurchaseRequisitionConfirmationHistory>();
            tpPurchaseRequisitionConfirmations = new HashSet<tpPurchaseRequisitionConfirmation>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionConfirmationEMailNotificationID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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
        public virtual prRequisitionConfirmationRuleStep prRequisitionConfirmationRuleStep { get; set; }

        public virtual ICollection<rpPurchaseRequisitionConfirmationHistory> rpPurchaseRequisitionConfirmationHistorys { get; set; }
        public virtual ICollection<tpPurchaseRequisitionConfirmation> tpPurchaseRequisitionConfirmations { get; set; }
    }
}
