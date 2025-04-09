using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExchangeRateLine")]
    public partial class trExchangeRateLine
    {
        public trExchangeRateLine()
        {
        }

        [Key]
        [Required]
        public Guid ExchangeRateLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public double EndOfDayRate { get; set; }

        [Required]
        public Guid ExchangeRateHeaderID { get; set; }

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
        public virtual trExchangeRateHeader trExchangeRateHeader { get; set; }

    }
}
