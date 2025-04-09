using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdReconciliation")]
    public partial class cdReconciliation
    {
        public cdReconciliation()
        {
            cdReconciliationDescs = new HashSet<cdReconciliationDesc>();
            prCurrAccReconciliationContactReportss = new HashSet<prCurrAccReconciliationContactReports>();
            trCurrAccReconciliationReports = new HashSet<trCurrAccReconciliationReport>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReconciliationCode { get; set; }

        [Required]
        public byte ReconciliationTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReportFileName { get; set; }

        [Required]
        public bool LockTransactions { get; set; }

        [Required]
        public bool ReportByCurrencyDetail { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsReconciliationType bsReconciliationType { get; set; }

        public virtual ICollection<cdReconciliationDesc> cdReconciliationDescs { get; set; }
        public virtual ICollection<prCurrAccReconciliationContactReports> prCurrAccReconciliationContactReportss { get; set; }
        public virtual ICollection<trCurrAccReconciliationReport> trCurrAccReconciliationReports { get; set; }
    }
}
