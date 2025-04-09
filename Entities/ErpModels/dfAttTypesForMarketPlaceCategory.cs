using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfAttTypesForMarketPlaceCategory")]
    public partial class dfAttTypesForMarketPlaceCategory
    {
        public dfAttTypesForMarketPlaceCategory()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode01 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode02 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode03 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode04 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AttTypeCode05 { get; set; }

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
