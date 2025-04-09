using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentLine")]
    public partial class e_OutboxShipmentLine
    {
        public e_OutboxShipmentLine()
        {
        }

        [Key]
        [Required]
        public Guid OutboxShipmentLineID { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [Required]
        public int LineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ItemName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SellersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BuyersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufacturersItemIdentificationID { get; set; }

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

        public string LineNote { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CompanyBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Navigation Properties
        public virtual e_OutboxShipmentHeader e_OutboxShipmentHeader { get; set; }

    }
}
