using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trVehicleLoadingLine")]
    public partial class trVehicleLoadingLine
    {
        public trVehicleLoadingLine()
        {
            tpVehicleLoadingLineDeliveryStatuss = new HashSet<tpVehicleLoadingLineDeliveryStatus>();
        }

        [Key]
        [Required]
        public Guid VehicleLoadingLineID { get; set; }

        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [Required]
        public Guid VehicleLoadingHeaderID { get; set; }

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
        public virtual trShipmentHeader trShipmentHeader { get; set; }
        public virtual trVehicleLoadingHeader trVehicleLoadingHeader { get; set; }

        public virtual ICollection<tpVehicleLoadingLineDeliveryStatus> tpVehicleLoadingLineDeliveryStatuss { get; set; }
    }
}
