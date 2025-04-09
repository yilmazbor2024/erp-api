using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpShipmentUBLExtensions")]
    public partial class tpShipmentUBLExtensions
    {
        public tpShipmentUBLExtensions()
        {
        }

        [Key]
        [Required]
        public Guid ShipmentUBLExtensionsID { get; set; }

        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UBLExtensionField { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SchemeID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldValue { get; set; }

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
        public virtual bsUBLExtensions bsUBLExtensions { get; set; }

    }
}
