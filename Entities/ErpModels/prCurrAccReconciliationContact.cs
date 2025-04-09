using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccReconciliationContact")]
    public partial class prCurrAccReconciliationContact
    {
        public prCurrAccReconciliationContact()
        {
            prCurrAccReconciliationContactReportss = new HashSet<prCurrAccReconciliationContactReports>();
            trCurrAccReconciliationEMailNotifications = new HashSet<trCurrAccReconciliationEMailNotification>();
            trCurrAccReconciliationReportConfirmations = new HashSet<trCurrAccReconciliationReportConfirmation>();
        }

        [Key]
        [Required]
        public Guid ReconciliationContactID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ContactID { get; set; }

        public Guid? MobilePhoneCommunicationID { get; set; }

        public Guid? EMailCommunicationID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

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
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual prCurrAccCommunication prCurrAccCommunication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prCurrAccReconciliationContactReports> prCurrAccReconciliationContactReportss { get; set; }
        public virtual ICollection<trCurrAccReconciliationEMailNotification> trCurrAccReconciliationEMailNotifications { get; set; }
        public virtual ICollection<trCurrAccReconciliationReportConfirmation> trCurrAccReconciliationReportConfirmations { get; set; }
    }
}
