using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trDepartmentReceiptLine")]
    public partial class trDepartmentReceiptLine
    {
        public trDepartmentReceiptLine()
        {
        }

        [Key]
        [Required]
        public Guid DepartmentReceiptLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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

        [Required]
        public double Qty { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        public string LineDescription { get; set; }

        [Required]
        public byte ItemDeliveryStatus { get; set; }

        [Required]
        public Guid DepartmentReceiptHeaderID { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        // Navigation Properties
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual trDepartmentReceiptHeader trDepartmentReceiptHeader { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

    }
}
