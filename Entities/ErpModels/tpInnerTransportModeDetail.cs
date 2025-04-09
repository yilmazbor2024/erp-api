using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInnerTransportModeDetail")]
    public partial class tpInnerTransportModeDetail
    {
        public tpInnerTransportModeDetail()
        {
        }

        [Key]
        [Required]
        public Guid InnerHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VesselID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VesselName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RadioCallSignID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShipsRequirements { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GrossTonnageMeasure { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NetTonnageMeasure { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RegistryCertificateDocumentReference { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RegistryPortLocation { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TrainID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RailCarID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LicensePlateID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AircraftID { get; set; }

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
        public virtual cdVehicle cdVehicle { get; set; }
        public virtual trInnerHeader trInnerHeader { get; set; }

    }
}
