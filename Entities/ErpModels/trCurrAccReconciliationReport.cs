using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCurrAccReconciliationReport")]
    public partial class trCurrAccReconciliationReport
    {
        public trCurrAccReconciliationReport()
        {
            trCurrAccReconciliationEMailNotifications = new HashSet<trCurrAccReconciliationEMailNotification>();
            trCurrAccReconciliationReportConfirmations = new HashSet<trCurrAccReconciliationReportConfirmation>();
        }

        [Key]
        [Required]
        public Guid CurrAccReconciliationReportsID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReconciliationCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        public string FilterString { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public bool IsRejected { get; set; }

        [Required]
        public DateTime RejectDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RejectedUserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RejectReasonDescription { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClosedUserName { get; set; }

        public string ReportData { get; set; }

        public string SummaryData { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdReconciliation cdReconciliation { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trCurrAccReconciliationEMailNotification> trCurrAccReconciliationEMailNotifications { get; set; }
        public virtual ICollection<trCurrAccReconciliationReportConfirmation> trCurrAccReconciliationReportConfirmations { get; set; }
    }
}
