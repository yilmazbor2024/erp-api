using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdVehicleType")]
    public partial class cdVehicleType
    {
        public cdVehicleType()
        {
            cdVehicles = new HashSet<cdVehicle>();
            cdVehicleTypeDescs = new HashSet<cdVehicleTypeDesc>();
            trAgentContractVehicles = new HashSet<trAgentContractVehicle>();
            trAgentReservationVehicleDetails = new HashSet<trAgentReservationVehicleDetail>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleTypeCode { get; set; }

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

        public virtual ICollection<cdVehicle> cdVehicles { get; set; }
        public virtual ICollection<cdVehicleTypeDesc> cdVehicleTypeDescs { get; set; }
        public virtual ICollection<trAgentContractVehicle> trAgentContractVehicles { get; set; }
        public virtual ICollection<trAgentReservationVehicleDetail> trAgentReservationVehicleDetails { get; set; }
    }
}
