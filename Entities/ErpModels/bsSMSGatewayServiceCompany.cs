using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSMSGatewayServiceCompany")]
    public partial class bsSMSGatewayServiceCompany
    {
        public bsSMSGatewayServiceCompany()
        {
            cdSMSGatewayServices = new HashSet<cdSMSGatewayService>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SMSGatewayServiceCompanyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SMSGatewayServiceCompanyName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdSMSGatewayService> cdSMSGatewayServices { get; set; }
    }
}
