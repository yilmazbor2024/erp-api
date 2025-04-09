using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpVehicleLoadingLineDeliveryStatus")]
    public partial class tpVehicleLoadingLineDeliveryStatus
    {
        public tpVehicleLoadingLineDeliveryStatus()
        {
        }

        [Key]
        [Required]
        public Guid VehicleLoadingLineID { get; set; }

        [Key]
        [Required]
        public Guid ShipmentLineID { get; set; }

        [Required]
        public double DeliveredQty { get; set; }

        [Required]
        public double UnDeliveredQty { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnDeliveryReasonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

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
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual trVehicleLoadingLine trVehicleLoadingLine { get; set; }
        public virtual trShipmentLine trShipmentLine { get; set; }
        public virtual cdUnDeliveryReason cdUnDeliveryReason { get; set; }

    }
}
