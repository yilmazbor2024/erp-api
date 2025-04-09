using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerOrderLine")]
    public partial class trInnerOrderLine
    {
        public trInnerOrderLine()
        {
            tpInnerOrderITAttributes = new HashSet<tpInnerOrderITAttribute>();
            trInnerLines = new HashSet<trInnerLine>();
        }

        [Key]
        [Required]
        public Guid InnerOrderLineID { get; set; }

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
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [Required]
        public double CancelQty1 { get; set; }

        [Required]
        public double CancelQty2 { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string OrderCancelReasonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToSectionCode { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public object BaseProcessCode { get; set; }

        [Required]
        public object BaseOrderNumber { get; set; }

        [Required]
        public int BaseCustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BaseCustomerCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BaseStoreCode { get; set; }

        [Required]
        public double SurplusOrderQtyToleranceRate { get; set; }

        public Guid? BaseCustomerOrderLineID { get; set; }

        [Required]
        public Guid InnerOrderHeaderID { get; set; }

        [Required]
        public int InnerOrderLineSumID { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trInnerOrderHeader trInnerOrderHeader { get; set; }
        public virtual cdOrderCancelReason cdOrderCancelReason { get; set; }

        public virtual ICollection<tpInnerOrderITAttribute> tpInnerOrderITAttributes { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
    }
}
