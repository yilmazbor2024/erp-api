using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderLineExtension")]
    public partial class tpOrderLineExtension
    {
        public tpOrderLineExtension()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public bool IsDeliveryAssigned { get; set; }

        [Required]
        public DateTime DeliveryAssignDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DeliveryAssignUserName { get; set; }

        [Required]
        public bool IsShowroomProduct { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OrderStatusCode { get; set; }

        [Required]
        public double OrderStatusQty1 { get; set; }

        [Required]
        public double OrderStatusQty2 { get; set; }

        // Navigation Properties
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdOrderStatus cdOrderStatus { get; set; }

    }
}
