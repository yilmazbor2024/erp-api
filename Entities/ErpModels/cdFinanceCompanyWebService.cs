using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFinanceCompanyWebService")]
    public partial class cdFinanceCompanyWebService
    {
        public cdFinanceCompanyWebService()
        {
            cdFinanceCompanyWebServiceDescs = new HashSet<cdFinanceCompanyWebServiceDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FinanceCompanyWebServiceCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WebServiceAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentApplicationFilePath { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentApplicationName { get; set; }

        [Required]
        public int IdentitySharingSystemQueryPeriod { get; set; }

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

        public virtual ICollection<cdFinanceCompanyWebServiceDesc> cdFinanceCompanyWebServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
    }
}
