using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyEmailDefault")]
    public partial class dfCompanyEmailDefault
    {
        public dfCompanyEmailDefault()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte EMailTypeCode { get; set; }

        [Required]
        public byte MailServiceType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DisplayName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MailHostingAddress { get; set; }

        [Required]
        public int MailHostingAddressPort { get; set; }

        [Required]
        public bool IsSSL { get; set; }

        [Required]
        public bool IsOldExchange { get; set; }

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
        public virtual bsEmailType bsEmailType { get; set; }

    }
}
