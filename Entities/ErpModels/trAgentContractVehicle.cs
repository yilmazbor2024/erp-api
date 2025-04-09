using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentContractVehicle")]
    public partial class trAgentContractVehicle
    {
        public trAgentContractVehicle()
        {
        }

        [Key]
        [Required]
        public Guid AgentContractVehicleID { get; set; }

        [Required]
        public Guid AgentContractHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NationalityCode { get; set; }

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
        public virtual trAgentContractHeader trAgentContractHeader { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdVehicleType cdVehicleType { get; set; }
        public virtual cdNationality cdNationality { get; set; }
        public virtual cdExchangeType cdExchangeType { get; set; }

    }
}
