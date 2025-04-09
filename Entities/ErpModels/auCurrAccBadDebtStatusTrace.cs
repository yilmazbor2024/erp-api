using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCurrAccBadDebtStatusTrace")]
    public partial class auCurrAccBadDebtStatusTrace
    {
        public auCurrAccBadDebtStatusTrace()
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
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public byte DebtStatusTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BadDebtReasonCode { get; set; }

        [Required]
        public bool DenyRetailSale { get; set; }

        [Required]
        public bool DenyInstalmentSale { get; set; }

        [Required]
        public bool DenyReturnRetailSale { get; set; }

        [Required]
        public bool DenyReturnInstalmentSale { get; set; }

        [Required]
        public bool DenyInstalmentPayment { get; set; }

        [Required]
        public bool DenyGuarantor { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
