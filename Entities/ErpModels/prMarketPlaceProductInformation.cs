using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMarketPlaceProductInformation")]
    public partial class prMarketPlaceProductInformation
    {
        public prMarketPlaceProductInformation()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Title { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SubTitle { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CategoryFullName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BasePricePriceGroupCode { get; set; }

        [Required]
        public byte BasePriceBasePriceCode { get; set; }

        [Required]
        public DateTime SaleStartDate { get; set; }

        [Required]
        public DateTime SaleEndDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductCondition { get; set; }

        [Required]
        public int PrepairingDay { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShipmentTemplateName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductMarketPlaceID { get; set; }

        [Required]
        public bool AvailableOnMarketPlace { get; set; }

        [Required]
        public byte SaleStatus { get; set; }

        [Required]
        public DateTime LastSendDate { get; set; }

        [Required]
        public byte LastSendStatus { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LastSendStatusDesc { get; set; }

        [Required]
        public byte ProductListingType { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual bsMarketPlace bsMarketPlace { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }

    }
}
