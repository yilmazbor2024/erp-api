using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOnlineDBSWebService")]
    public partial class cdOnlineDBSWebService
    {
        public cdOnlineDBSWebService()
        {
            cdOnlineDBSWebServiceDescs = new HashSet<cdOnlineDBSWebServiceDesc>();
            prCustomerDBSAccounts = new HashSet<prCustomerDBSAccount>();
            prOnlineDBSLimits = new HashSet<prOnlineDBSLimit>();
            prOnlineDBSLimitHistorys = new HashSet<prOnlineDBSLimitHistory>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineDBSWebServiceCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApiKey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SecretKey { get; set; }

        [Required]
        public int BranchID { get; set; }

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

        public virtual ICollection<cdOnlineDBSWebServiceDesc> cdOnlineDBSWebServiceDescs { get; set; }
        public virtual ICollection<prCustomerDBSAccount> prCustomerDBSAccounts { get; set; }
        public virtual ICollection<prOnlineDBSLimit> prOnlineDBSLimits { get; set; }
        public virtual ICollection<prOnlineDBSLimitHistory> prOnlineDBSLimitHistorys { get; set; }
    }
}
