using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentPerformanceBonusLine")]
    public partial class trAgentPerformanceBonusLine
    {
        public trAgentPerformanceBonusLine()
        {
        }

        [Key]
        [Required]
        public Guid AgentPerformanceBonusLineID { get; set; }

        [Required]
        public Guid AgentPerformanceBonusHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public decimal MaxAmount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BonusCurrencyCode { get; set; }

        [Required]
        public byte BonusExchangeTypeCode { get; set; }

        [Required]
        public decimal BonusAmount { get; set; }

        [Required]
        public double Rate { get; set; }

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
        public virtual trAgentPerformanceBonusHeader trAgentPerformanceBonusHeader { get; set; }
        public virtual cdExchangeType cdExchangeType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
