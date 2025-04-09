using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auProcessFlowDenyTrace")]
    public partial class auProcessFlowDenyTrace
    {
        public auProcessFlowDenyTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProcessFlowDenyReasonCode { get; set; }

        [Required]
        public bool AllowReturnTransactions { get; set; }

        [Required]
        public bool IsDeny { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
