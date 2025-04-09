using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBulkMailServiceProvider")]
    public partial class bsBulkMailServiceProvider
    {
        public bsBulkMailServiceProvider()
        {
            bsBulkMailServiceProviderDescs = new HashSet<bsBulkMailServiceProviderDesc>();
            dfBulkMailServiceProviderAccounts = new HashSet<dfBulkMailServiceProviderAccount>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
        }

        [Key]
        [Required]
        public byte BulkMailServiceProviderCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBulkMailServiceProviderDesc> bsBulkMailServiceProviderDescs { get; set; }
        public virtual ICollection<dfBulkMailServiceProviderAccount> dfBulkMailServiceProviderAccounts { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
    }
}
