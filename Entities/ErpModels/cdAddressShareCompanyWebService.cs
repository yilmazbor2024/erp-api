using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdAddressShareCompanyWebService")]
    public partial class cdAddressShareCompanyWebService
    {
        public cdAddressShareCompanyWebService()
        {
            cdAddressShareCompanyWebServiceDescs = new HashSet<cdAddressShareCompanyWebServiceDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AddressShareCompanyWebServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object WebServiceAddress { get; set; }

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

        public virtual ICollection<cdAddressShareCompanyWebServiceDesc> cdAddressShareCompanyWebServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
    }
}
