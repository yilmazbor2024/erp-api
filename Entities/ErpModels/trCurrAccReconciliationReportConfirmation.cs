using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCurrAccReconciliationReportConfirmation")]
    public partial class trCurrAccReconciliationReportConfirmation
    {
        public trCurrAccReconciliationReportConfirmation()
        {
        }

        [Key]
        [Required]
        public Guid CurrAccReconciliationReportsID { get; set; }

        [Required]
        public Guid ReconciliationContactID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConfirmationUserName { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsRejected { get; set; }

        [Required]
        public DateTime RejectDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RejectReasonDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IPAddress { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ClientName { get; set; }

        public Guid? CurrAccReconciliationEMailNotificationID { get; set; }

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
        public virtual trCurrAccReconciliationReport trCurrAccReconciliationReport { get; set; }
        public virtual prCurrAccReconciliationContact prCurrAccReconciliationContact { get; set; }
        public virtual trCurrAccReconciliationEMailNotification trCurrAccReconciliationEMailNotification { get; set; }

    }
}
