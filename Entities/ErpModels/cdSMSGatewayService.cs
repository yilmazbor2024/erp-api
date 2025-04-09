using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSMSGatewayService")]
    public partial class cdSMSGatewayService
    {
        public cdSMSGatewayService()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClientCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Gateway { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Originator { get; set; }

        [Required]
        public bool UseInteractiveSMS { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ServiceProviderCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SMSGatewayServiceDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SMSGatewayServiceCompanyCode { get; set; }

        [Required]
        public byte SendType { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SMSGatewayMessageType { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SMSGatewayRecipientType { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsSMSGatewayServiceCompany bsSMSGatewayServiceCompany { get; set; }
        public virtual bsGatewayServiceProvider bsGatewayServiceProvider { get; set; }

    }
}
