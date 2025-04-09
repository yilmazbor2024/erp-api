using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpExternalItemFileLine")]
    public partial class rpExternalItemFileLine
    {
        public rpExternalItemFileLine()
        {
        }

        [Key]
        [Required]
        public Guid ExternalItemFileLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Barcode { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public double PriceExchangeRate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal PriceVI { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CostCenterCode { get; set; }

        [Required]
        public double LDisRate1 { get; set; }

        [Required]
        public double LDisRate2 { get; set; }

        [Required]
        public double LDisRate3 { get; set; }

        [Required]
        public double LDisRate4 { get; set; }

        [Required]
        public double LDisRate5 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt06 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt07 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt08 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt09 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ITAtt10 { get; set; }

        [Required]
        public int LineSumID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LotBarcode { get; set; }

        [Required]
        public object ImportFileNumber { get; set; }

        [Required]
        public object ExportFileNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProductSerialNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public Guid ExternalItemFileHeaderID { get; set; }

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
        public virtual rpExternalItemFileHeader rpExternalItemFileHeader { get; set; }

    }
}
