using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMarketPlaceCategoryConvert")]
    public partial class prMarketPlaceCategoryConvert
    {
        public prMarketPlaceCategoryConvert()
        {
        }

        [Key]
        [Required]
        public Guid MarketPlaceCategoryConvertID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttCode01 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode02 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttCode02 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttCode03 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttCode04 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttCode05 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MarketPlaceCategoryID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MarketPlaceCategoryName { get; set; }

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
        public virtual bsMarketPlace bsMarketPlace { get; set; }

    }
}
