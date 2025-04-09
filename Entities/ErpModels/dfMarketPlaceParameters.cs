using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfMarketPlaceParameters")]
    public partial class dfMarketPlaceParameters
    {
        public dfMarketPlaceParameters()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceURL { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string APIKey { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string APIPassword { get; set; }

        [Required]
        public bool UseItemAttTypeForCategoryConvert { get; set; }

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
        public virtual bsMarketPlace bsMarketPlace { get; set; }

    }
}
