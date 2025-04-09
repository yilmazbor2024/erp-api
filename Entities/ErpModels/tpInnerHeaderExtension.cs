using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInnerHeaderExtension")]
    public partial class tpInnerHeaderExtension
    {
        public tpInnerHeaderExtension()
        {
        }

        [Key]
        [Required]
        public Guid InnerHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LogisticsCompanyBOL { get; set; }

        [Required]
        public byte ShipmentTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EShipmentNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MainEShipmentNumber { get; set; }

        public Guid? MainInnerHeaderID { get; set; }

        [Required]
        public byte EShipmentStatusCode { get; set; }

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
        public virtual trInnerHeader trInnerHeader { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual bsShipmentType bsShipmentType { get; set; }
        public virtual bsEShipmentStatus bsEShipmentStatus { get; set; }

    }
}
