using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpShipmentReturn")]
    public partial class tpShipmentReturn
    {
        public tpShipmentReturn()
        {
        }

        [Key]
        [Required]
        public Guid ShipmentLineID { get; set; }

        [Key]
        [Required]
        public Guid ReturnShipmentLineID { get; set; }

        [Required]
        public double ReturnQty1 { get; set; }

        [Required]
        public double ReturnQty2 { get; set; }

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
