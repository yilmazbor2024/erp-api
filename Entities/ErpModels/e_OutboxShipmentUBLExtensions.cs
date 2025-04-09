using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentUBLExtensions")]
    public partial class e_OutboxShipmentUBLExtensions
    {
        public e_OutboxShipmentUBLExtensions()
        {
        }

        [Key]
        [Required]
        public Guid ShipmentUBLExtensionsID { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UBLExtensionField { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SchemeID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldValue { get; set; }

        // Navigation Properties
        public virtual e_OutboxShipmentHeader e_OutboxShipmentHeader { get; set; }

    }
}
