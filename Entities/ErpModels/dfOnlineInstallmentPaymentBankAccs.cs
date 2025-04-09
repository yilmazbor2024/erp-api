using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOnlineInstallmentPaymentBankAccs")]
    public partial class dfOnlineInstallmentPaymentBankAccs
    {
        public dfOnlineInstallmentPaymentBankAccs()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RecipientName { get; set; }

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
        public virtual dfOnlineInstallmentPaymentParameters dfOnlineInstallmentPaymentParameters { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
