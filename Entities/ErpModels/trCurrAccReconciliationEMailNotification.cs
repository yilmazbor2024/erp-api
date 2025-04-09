using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCurrAccReconciliationEMailNotification")]
    public partial class trCurrAccReconciliationEMailNotification
    {
        public trCurrAccReconciliationEMailNotification()
        {
            trCurrAccReconciliationReportConfirmations = new HashSet<trCurrAccReconciliationReportConfirmation>();
        }

        [Key]
        [Required]
        public Guid CurrAccReconciliationEMailNotificationID { get; set; }

        [Required]
        public Guid CurrAccReconciliationReportsID { get; set; }

        [Required]
        public Guid ReconciliationContactID { get; set; }

        [Required]
        public DateTime EmailSendDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EmailSendUserName { get; set; }

        [Required]
        public DateTime EMailLinkExpireDate { get; set; }

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
        public virtual prCurrAccReconciliationContact prCurrAccReconciliationContact { get; set; }
        public virtual trCurrAccReconciliationReport trCurrAccReconciliationReport { get; set; }

        public virtual ICollection<trCurrAccReconciliationReportConfirmation> trCurrAccReconciliationReportConfirmations { get; set; }
    }
}
