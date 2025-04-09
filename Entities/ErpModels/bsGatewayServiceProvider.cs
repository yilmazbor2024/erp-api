using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsGatewayServiceProvider")]
    public partial class bsGatewayServiceProvider
    {
        public bsGatewayServiceProvider()
        {
            cdSMSGatewayServices = new HashSet<cdSMSGatewayService>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ServiceProviderCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceProviderDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdSMSGatewayService> cdSMSGatewayServices { get; set; }
    }
}
