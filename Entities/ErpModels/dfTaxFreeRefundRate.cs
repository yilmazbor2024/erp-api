using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfTaxFreeRefundRate")]
    public partial class dfTaxFreeRefundRate
    {
        public dfTaxFreeRefundRate()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Key]
        [Required]
        public float VatRate { get; set; }

        [Key]
        [Required]
        public decimal SaleAmountLimitWithVat { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public decimal TaxRefund { get; set; }

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
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
