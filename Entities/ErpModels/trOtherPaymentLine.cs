using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOtherPaymentLine")]
    public partial class trOtherPaymentLine
    {
        public trOtherPaymentLine()
        {
            tpOtherPaymentFTAttributes = new HashSet<tpOtherPaymentFTAttribute>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trOtherPaymentLineCurrencys = new HashSet<trOtherPaymentLineCurrency>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid OtherPaymentLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public Guid OtherPaymentHeaderID { get; set; }

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
        public virtual trOtherPaymentHeader trOtherPaymentHeader { get; set; }

        public virtual ICollection<tpOtherPaymentFTAttribute> tpOtherPaymentFTAttributes { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trOtherPaymentLineCurrency> trOtherPaymentLineCurrencys { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
