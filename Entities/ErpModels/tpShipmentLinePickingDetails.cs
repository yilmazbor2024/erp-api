using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpShipmentLinePickingDetails")]
    public partial class tpShipmentLinePickingDetails
    {
        public tpShipmentLinePickingDetails()
        {
        }

        [Key]
        [Required]
        public Guid ShipmentLinePickingDetailID { get; set; }

        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [Required]
        public Guid ShipmentLineID { get; set; }

        [Required]
        public DateTime PickingDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PackageNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackagingTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PackageBrandCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackageVolumeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WeightUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWeight { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        public double Qty { get; set; }

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
        public virtual bsPackagingType bsPackagingType { get; set; }
        public virtual cdPackageVolume cdPackageVolume { get; set; }
        public virtual cdPackageBrand cdPackageBrand { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual trShipmentLine trShipmentLine { get; set; }

    }
}
