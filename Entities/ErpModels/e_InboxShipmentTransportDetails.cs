using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentTransportDetails")]
    public partial class e_InboxShipmentTransportDetails
    {
        public e_InboxShipmentTransportDetails()
        {
        }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransportModeCode { get; set; }

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

        // Navigation Properties
        public virtual e_InboxShipmentHeader e_InboxShipmentHeader { get; set; }

    }
}
