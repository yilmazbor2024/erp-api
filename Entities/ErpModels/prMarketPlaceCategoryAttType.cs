using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMarketPlaceCategoryAttType")]
    public partial class prMarketPlaceCategoryAttType
    {
        public prMarketPlaceCategoryAttType()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MarketPlaceCategoryID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MarketPlaceCategoryAttTypeID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MarketPlaceCategoryAttTypeName { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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
