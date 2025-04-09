using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOnlineInstallmentPaymentParameters")]
    public partial class dfOnlineInstallmentPaymentParameters
    {
        public dfOnlineInstallmentPaymentParameters()
        {
            dfOnlineInstallmentPaymentBankAccss = new HashSet<dfOnlineInstallmentPaymentBankAccs>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BankPaymentCodePrefix { get; set; }

        [Required]
        public bool SendSMSForBankPaymentCode { get; set; }

        [Required]
        public bool SendSMSForBankPaymentRealised { get; set; }

        [Required]
        public bool AcceptMissingBankPaymentAmount { get; set; }

        [Required]
        public bool AcceptOverBankPaymentAmount { get; set; }

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

        public virtual ICollection<dfOnlineInstallmentPaymentBankAccs> dfOnlineInstallmentPaymentBankAccss { get; set; }
    }
}
