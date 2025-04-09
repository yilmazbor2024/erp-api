using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDeliveryCompanyMarketPlaceMapping")]
    public partial class prDeliveryCompanyMarketPlaceMapping
    {
        public prDeliveryCompanyMarketPlaceMapping()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentCompanyID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ShipmentCompanyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShipmentCompanyShortName { get; set; }

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
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual bsMarketPlace bsMarketPlace { get; set; }

    }
}
