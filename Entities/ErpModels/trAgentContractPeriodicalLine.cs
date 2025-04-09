using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentContractPeriodicalLine")]
    public partial class trAgentContractPeriodicalLine
    {
        public trAgentContractPeriodicalLine()
        {
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
        }

        [Key]
        [Required]
        public Guid AgentContractPeriodicalLineID { get; set; }

        [Required]
        public Guid AgentContractHeaderID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PeriodDescription { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string TypeDescription { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual cdExchangeType cdExchangeType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trAgentContractHeader trAgentContractHeader { get; set; }

        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
    }
}
