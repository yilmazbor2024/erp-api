using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auPurchaseRequisitionLineRevisionTrace")]
    public partial class auPurchaseRequisitionLineRevisionTrace
    {
        public auPurchaseRequisitionLineRevisionTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Required]
        public byte RevisionNumber { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OldValue { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NewValue { get; set; }

    }
}
