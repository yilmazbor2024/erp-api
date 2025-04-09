using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionConfirmation")]
    public partial class tpPurchaseRequisitionConfirmation
    {
        public tpPurchaseRequisitionConfirmation()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

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

        public Guid? PurchaseRequisitionConfirmationEMailNotificationID { get; set; }

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
        public virtual trPurchaseRequisitionConfirmationEMailNotification trPurchaseRequisitionConfirmationEMailNotification { get; set; }
        public virtual prRequisitionConfirmationRuleStep prRequisitionConfirmationRuleStep { get; set; }

    }
}
