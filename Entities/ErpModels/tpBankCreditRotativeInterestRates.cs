using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBankCreditRotativeInterestRates")]
    public partial class tpBankCreditRotativeInterestRates
    {
        public tpBankCreditRotativeInterestRates()
        {
        }

        [Key]
        [Required]
        public Guid BankCreditHeaderID { get; set; }

        [Key]
        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public float InterestRate { get; set; }

        [Required]
        public float KKDFRate { get; set; }

        [Required]
        public float BSMVRate { get; set; }

        [Required]
        public Guid JournalHeaderID { get; set; }

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

        // Navigation Properties
        public virtual trBankCreditHeader trBankCreditHeader { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

    }
}
