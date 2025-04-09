using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdVehicle")]
    public partial class cdVehicle
    {
        public cdVehicle()
        {
            dfStoreDefaults = new HashSet<dfStoreDefault>();
            prVehicleDriverss = new HashSet<prVehicleDrivers>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            tpInnerTransportModeDetails = new HashSet<tpInnerTransportModeDetail>();
            tpShipmentTransportModeDetails = new HashSet<tpShipmentTransportModeDetail>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
            trVehicleUnLoadingHeaders = new HashSet<trVehicleUnLoadingHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Brand { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Model { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LicensePlate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [Required]
        public double MaxVolume { get; set; }

        [Required]
        public double MaxWeight { get; set; }

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

        // Navigation Properties
        public virtual cdVehicleType cdVehicleType { get; set; }

        public virtual ICollection<dfStoreDefault> dfStoreDefaults { get; set; }
        public virtual ICollection<prVehicleDrivers> prVehicleDriverss { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<tpInnerTransportModeDetail> tpInnerTransportModeDetails { get; set; }
        public virtual ICollection<tpShipmentTransportModeDetail> tpShipmentTransportModeDetails { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
        public virtual ICollection<trVehicleUnLoadingHeader> trVehicleUnLoadingHeaders { get; set; }
    }
}
