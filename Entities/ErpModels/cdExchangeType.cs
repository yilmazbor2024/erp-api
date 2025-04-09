using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExchangeType")]
    public partial class cdExchangeType
    {
        public cdExchangeType()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdExchangeTypeDescs = new HashSet<cdExchangeTypeDesc>();
            cdUniFreeTenderTypes = new HashSet<cdUniFreeTenderType>();
            dfAirportExchangeRateWidgetParameterss = new HashSet<dfAirportExchangeRateWidgetParameters>();
            dfDufryCompanys = new HashSet<dfDufryCompany>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prRelationalPriceGroupss = new HashSet<prRelationalPriceGroups>();
            trAgentContractDeservedLines = new HashSet<trAgentContractDeservedLine>();
            trAgentContractPeriodicalLines = new HashSet<trAgentContractPeriodicalLine>();
            trAgentContractVehicles = new HashSet<trAgentContractVehicle>();
            trAgentContractVisitFrequencyLines = new HashSet<trAgentContractVisitFrequencyLine>();
            trAgentPerformanceBonusLines = new HashSet<trAgentPerformanceBonusLine>();
        }

        [Key]
        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdExchangeTypeDesc> cdExchangeTypeDescs { get; set; }
        public virtual ICollection<cdUniFreeTenderType> cdUniFreeTenderTypes { get; set; }
        public virtual ICollection<dfAirportExchangeRateWidgetParameters> dfAirportExchangeRateWidgetParameterss { get; set; }
        public virtual ICollection<dfDufryCompany> dfDufryCompanys { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prRelationalPriceGroups> prRelationalPriceGroupss { get; set; }
        public virtual ICollection<trAgentContractDeservedLine> trAgentContractDeservedLines { get; set; }
        public virtual ICollection<trAgentContractPeriodicalLine> trAgentContractPeriodicalLines { get; set; }
        public virtual ICollection<trAgentContractVehicle> trAgentContractVehicles { get; set; }
        public virtual ICollection<trAgentContractVisitFrequencyLine> trAgentContractVisitFrequencyLines { get; set; }
        public virtual ICollection<trAgentPerformanceBonusLine> trAgentPerformanceBonusLines { get; set; }
    }
}
