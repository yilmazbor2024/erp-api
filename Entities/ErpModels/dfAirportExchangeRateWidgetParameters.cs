using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfAirportExchangeRateWidgetParameters")]
    public partial class dfAirportExchangeRateWidgetParameters
    {
        public dfAirportExchangeRateWidgetParameters()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [Required]
        public short PosTerminalID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LogoFilePath { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FreeText { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BaseCurrencyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CurrencyCodesToShow { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public byte MaxFontSize { get; set; }

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
        public virtual cdExchangeType cdExchangeType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }

    }
}
