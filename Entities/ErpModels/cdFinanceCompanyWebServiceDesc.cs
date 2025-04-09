using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFinanceCompanyWebServiceDesc")]
    public partial class cdFinanceCompanyWebServiceDesc
    {
        public cdFinanceCompanyWebServiceDesc()
        {
        }

        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FinanceCompanyWebServiceCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FinanceCompanyWebServiceDescription { get; set; }

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
        public virtual cdFinanceCompanyWebService cdFinanceCompanyWebService { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
