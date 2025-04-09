using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsMMSBusinessPartner")]
    public partial class bsMMSBusinessPartner
    {
        public bsMMSBusinessPartner()
        {
            cdMMSBusinessPartnerServices = new HashSet<cdMMSBusinessPartnerService>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MMSBusinessPartnerCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdMMSBusinessPartnerService> cdMMSBusinessPartnerServices { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
    }
}
