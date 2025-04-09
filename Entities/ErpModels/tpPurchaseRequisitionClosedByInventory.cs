using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionClosedByInventory")]
    public partial class tpPurchaseRequisitionClosedByInventory
    {
        public tpPurchaseRequisitionClosedByInventory()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionClosedByInventoryID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PurchasingAgentCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public double InventoryQty { get; set; }

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
        public virtual cdPurchasingAgent cdPurchasingAgent { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }

    }
}
