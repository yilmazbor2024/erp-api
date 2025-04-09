using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prInteractiveSMSApplications")]
    public partial class prInteractiveSMSApplications
    {
        public prInteractiveSMSApplications()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GSMOperator { get; set; }

        [Key]
        [Required]
        public int PrefixID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Prefix { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Keyword { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ActionType { get; set; }

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
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
