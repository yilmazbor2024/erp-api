using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auReconciliationReportNotificationTrace")]
    public partial class auReconciliationReportNotificationTrace
    {
        public auReconciliationReportNotificationTrace()
        {
        }

        [Key]
        [Required]
        public Guid ReconciliationReportNotificationTraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public Guid CurrAccReconciliationReportsID { get; set; }

        [Required]
        public Guid ReconciliationContactID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EmailAdress { get; set; }

    }
}
