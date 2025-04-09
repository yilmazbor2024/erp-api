using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMarketPlaceCategoryAttConvert")]
    public partial class prMarketPlaceCategoryAttConvert
    {
        public prMarketPlaceCategoryAttConvert()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MarketPlaceCategoryAttTypeID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string NebimV3TypeValue { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string NebimV3Value { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MarketPlaceCategoryAttID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MarketPlaceCategoryAttName { get; set; }

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
        public virtual bsMarketPlace bsMarketPlace { get; set; }

    }
}
