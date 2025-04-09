using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccReconciliationContactReports")]
    public partial class prCurrAccReconciliationContactReports
    {
        public prCurrAccReconciliationContactReports()
        {
        }

        [Key]
        [Required]
        public Guid ReconciliationContactID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReconciliationCode { get; set; }

        [Required]
        public bool SendNotification { get; set; }

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
        public virtual cdReconciliation cdReconciliation { get; set; }

    }
}
