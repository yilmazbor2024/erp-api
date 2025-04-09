using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExchangeRateHeader")]
    public partial class trExchangeRateHeader
    {
        public trExchangeRateHeader()
        {
            trExchangeRateLines = new HashSet<trExchangeRateLine>();
        }

        [Key]
        [Required]
        public Guid ExchangeRateHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelationCurrencyCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public DateTime ExchangeDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

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

        public virtual ICollection<trExchangeRateLine> trExchangeRateLines { get; set; }
    }
}
