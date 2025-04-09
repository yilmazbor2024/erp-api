using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trShipmentLineBOM")]
    public partial class trShipmentLineBOM
    {
        public trShipmentLineBOM()
        {
        }

        [Key]
        [Required]
        public int ShipmentLineBOMID { get; set; }

        [Key]
        [Required]
        public Guid ShipmentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BOMID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ParentBOMID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MainBOMCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BOMDescription { get; set; }

        [Required]
        public double BomQty { get; set; }

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
        public virtual trShipmentLine trShipmentLine { get; set; }

    }
}
