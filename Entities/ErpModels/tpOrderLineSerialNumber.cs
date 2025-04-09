using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderLineSerialNumber")]
    public partial class tpOrderLineSerialNumber
    {
        public tpOrderLineSerialNumber()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }

    }
}
