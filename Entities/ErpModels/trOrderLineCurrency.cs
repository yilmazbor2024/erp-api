using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderLineCurrency")]
    public partial class trOrderLineCurrency
    {
        public trOrderLineCurrency()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelationCurrencyCode { get; set; }

        [Required]
        public decimal PriceVI { get; set; }

        [Required]
        public decimal AmountVI { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal LDiscount1 { get; set; }

        [Required]
        public decimal LDiscount2 { get; set; }

        [Required]
        public decimal LDiscount3 { get; set; }

        [Required]
        public decimal LDiscount4 { get; set; }

        [Required]
        public decimal LDiscount5 { get; set; }

        [Required]
        public decimal LDiscountVI1 { get; set; }

        [Required]
        public decimal LDiscountVI2 { get; set; }

        [Required]
        public decimal LDiscountVI3 { get; set; }

        [Required]
        public decimal LDiscountVI4 { get; set; }

        [Required]
        public decimal LDiscountVI5 { get; set; }

        [Required]
        public decimal TDiscount1 { get; set; }

        [Required]
        public decimal TDiscount2 { get; set; }

        [Required]
        public decimal TDiscount3 { get; set; }

        [Required]
        public decimal TDiscount4 { get; set; }

        [Required]
        public decimal TDiscount5 { get; set; }

        [Required]
        public decimal TDiscountVI1 { get; set; }

        [Required]
        public decimal TDiscountVI2 { get; set; }

        [Required]
        public decimal TDiscountVI3 { get; set; }

        [Required]
        public decimal TDiscountVI4 { get; set; }

        [Required]
        public decimal TDiscountVI5 { get; set; }

        [Required]
        public decimal TaxBase { get; set; }

        [Required]
        public decimal Pct { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public decimal VatDeducation { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
