using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderCancelDetail")]
    public partial class tpOrderCancelDetail
    {
        public tpOrderCancelDetail()
        {
        }

        [Key]
        [Required]
        public Guid OrderCancelDetailID { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public Guid OrderCancelDetailHeaderID { get; set; }

        [Required]
        public double CancelQty1 { get; set; }

        [Required]
        public double CancelQty2 { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string OrderCancelReasonCode { get; set; }

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
        public virtual tpOrderCancelDetailHeader tpOrderCancelDetailHeader { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdOrderCancelReason cdOrderCancelReason { get; set; }

    }
}
