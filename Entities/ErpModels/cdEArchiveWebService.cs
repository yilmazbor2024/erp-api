using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEArchiveWebService")]
    public partial class cdEArchiveWebService
    {
        public cdEArchiveWebService()
        {
            cdEArchiveWebServiceDescs = new HashSet<cdEArchiveWebServiceDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfEArchiveWebServiceParameterss = new HashSet<dfEArchiveWebServiceParameters>();
            prEArchiveWebServiceCompanys = new HashSet<prEArchiveWebServiceCompany>();
            prEArchiveWebServiceOffices = new HashSet<prEArchiveWebServiceOffice>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EArchiveWebServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress2 { get; set; }

        [Required]
        public bool CheckCustomerEmailSyntax { get; set; }

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

        public virtual ICollection<cdEArchiveWebServiceDesc> cdEArchiveWebServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfEArchiveWebServiceParameters> dfEArchiveWebServiceParameterss { get; set; }
        public virtual ICollection<prEArchiveWebServiceCompany> prEArchiveWebServiceCompanys { get; set; }
        public virtual ICollection<prEArchiveWebServiceOffice> prEArchiveWebServiceOffices { get; set; }
    }
}
