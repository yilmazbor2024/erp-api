using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBankCreditType")]
    public partial class cdBankCreditType
    {
        public cdBankCreditType()
        {
            cdBankCreditTypeDescs = new HashSet<cdBankCreditTypeDesc>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCreditTypeCode { get; set; }

        [Required]
        public byte CreditTypeCode { get; set; }

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
        public virtual bsCreditType bsCreditType { get; set; }

        public virtual ICollection<cdBankCreditTypeDesc> cdBankCreditTypeDescs { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
    }
}
