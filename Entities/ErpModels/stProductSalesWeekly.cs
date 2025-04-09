using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stProductSalesWeekly")]
    public partial class stProductSalesWeekly
    {
        public stProductSalesWeekly()
        {
        }

        [Key]
        [Required]
        public long ProductSalesWeeklyID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SalesChannelCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short SalesYear { get; set; }

        [Required]
        public byte SalesQuarter { get; set; }

        [Required]
        public byte SalesMonth { get; set; }

        [Required]
        public byte SalesWeek { get; set; }

        [Required]
        public byte SalesIsoWeek { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

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
        public double SalesQty1 { get; set; }

        [Required]
        public decimal CompanySales { get; set; }

        [Required]
        public decimal LocalSales { get; set; }

    }
}
