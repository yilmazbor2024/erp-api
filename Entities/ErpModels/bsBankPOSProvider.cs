using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankPOSProvider")]
    public partial class bsBankPOSProvider
    {
        public bsBankPOSProvider()
        {
            prBankPOSProviderConverts = new HashSet<prBankPOSProviderConvert>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankPOSProviderCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankPOSProviderDescription { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prBankPOSProviderConvert> prBankPOSProviderConverts { get; set; }
    }
}
