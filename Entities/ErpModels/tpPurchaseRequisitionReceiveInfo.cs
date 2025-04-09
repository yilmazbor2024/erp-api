using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionReceiveInfo")]
    public partial class tpPurchaseRequisitionReceiveInfo
    {
        public tpPurchaseRequisitionReceiveInfo()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionReceiveInfoID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ReceivedBy { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public double ReceivedQty { get; set; }

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
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }

    }
}
