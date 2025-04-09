using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEMailService")]
    public partial class cdEMailService
    {
        public cdEMailService()
        {
            cdEMailServiceDescs = new HashSet<cdEMailServiceDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EMailServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClientCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceAddress { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SenderAddress { get; set; }

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

        public virtual ICollection<cdEMailServiceDesc> cdEMailServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
    }
}
