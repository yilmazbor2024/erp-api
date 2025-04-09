using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpProductLabel")]
    public partial class rpProductLabel
    {
        public rpProductLabel()
        {
        }

        [Key]
        [Required]
        public Guid ProductLabelLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ListName { get; set; }

        [Required]
        public DateTime ListDate { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LotBarcode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchBarcode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [Required]
        public int LabelQTy { get; set; }

        [Required]
        public byte BasePriceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BaseSalePriceCurrencyCode { get; set; }

        [Required]
        public decimal BaseSalePrice { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool PermitOtherUsers { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        public Guid? ApplicationLineID { get; set; }

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

    }
}
