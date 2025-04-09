using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerDBSAccount")]
    public partial class prCustomerDBSAccount
    {
        public prCustomerDBSAccount()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

       
        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Key]
        [Required]
        public byte CustomerTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerDBSAccountCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineDBSWebServiceCode { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

        [Required]
        public bool IsDefault { get; set; }

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
        public virtual cdBank cdBank { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOnlineDBSWebService cdOnlineDBSWebService { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
