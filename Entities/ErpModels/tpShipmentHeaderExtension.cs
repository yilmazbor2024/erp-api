using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpShipmentHeaderExtension")]
    public partial class tpShipmentHeaderExtension
    {
        public tpShipmentHeaderExtension()
        {
        }

        [Key]
        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [Required]
        public byte ShipmentTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EShipmentNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MainEShipmentNumber { get; set; }

        public Guid? MainShipmentHeaderID { get; set; }

        [Required]
        public byte EShipmentStatusCode { get; set; }

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
        public virtual bsShipmentType bsShipmentType { get; set; }
        public virtual bsEShipmentStatus bsEShipmentStatus { get; set; }

    }
}
