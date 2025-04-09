using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trGiftCardPaymentLine")]
    public partial class trGiftCardPaymentLine
    {
        public trGiftCardPaymentLine()
        {
            tpGiftCardPaymentFTAttributes = new HashSet<tpGiftCardPaymentFTAttribute>();
            trGiftCardPaymentLineCurrencys = new HashSet<trGiftCardPaymentLineCurrency>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid GiftCardPaymentLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public Guid GiftCardPaymentHeaderID { get; set; }

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
        public virtual cdGiftCard cdGiftCard { get; set; }
        public virtual trGiftCardPaymentHeader trGiftCardPaymentHeader { get; set; }

        public virtual ICollection<tpGiftCardPaymentFTAttribute> tpGiftCardPaymentFTAttributes { get; set; }
        public virtual ICollection<trGiftCardPaymentLineCurrency> trGiftCardPaymentLineCurrencys { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
