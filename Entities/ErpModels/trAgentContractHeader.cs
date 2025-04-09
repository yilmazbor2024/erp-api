using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentContractHeader")]
    public partial class trAgentContractHeader
    {
        public trAgentContractHeader()
        {
            tpInvoiceLineAgentPerformances = new HashSet<tpInvoiceLineAgentPerformance>();
            trAgentContractDeservedLines = new HashSet<trAgentContractDeservedLine>();
            trAgentContractPeriodicalLines = new HashSet<trAgentContractPeriodicalLine>();
            trAgentContractSpecialLines = new HashSet<trAgentContractSpecialLine>();
            trAgentContractStandartLines = new HashSet<trAgentContractStandartLine>();
            trAgentContractVehicles = new HashSet<trAgentContractVehicle>();
            trAgentContractVisitFrequencyLines = new HashSet<trAgentContractVisitFrequencyLine>();
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
        }

        [Key]
        [Required]
        public Guid AgentContractHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public DateTime FirstDate { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsInstant { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpInvoiceLineAgentPerformance> tpInvoiceLineAgentPerformances { get; set; }
        public virtual ICollection<trAgentContractDeservedLine> trAgentContractDeservedLines { get; set; }
        public virtual ICollection<trAgentContractPeriodicalLine> trAgentContractPeriodicalLines { get; set; }
        public virtual ICollection<trAgentContractSpecialLine> trAgentContractSpecialLines { get; set; }
        public virtual ICollection<trAgentContractStandartLine> trAgentContractStandartLines { get; set; }
        public virtual ICollection<trAgentContractVehicle> trAgentContractVehicles { get; set; }
        public virtual ICollection<trAgentContractVisitFrequencyLine> trAgentContractVisitFrequencyLines { get; set; }
        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
    }
}
