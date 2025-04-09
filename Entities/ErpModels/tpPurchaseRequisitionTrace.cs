using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionTrace")]
    public partial class tpPurchaseRequisitionTrace
    {
        public tpPurchaseRequisitionTrace()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionTraceID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EventType { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ValueStr { get; set; }

        [Required]
        public bool ValueByte { get; set; }

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

    }
}
