using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalLedgerEntryNumber")]
    public partial class trJournalLedgerEntryNumber
    {
        public trJournalLedgerEntryNumber()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public long LedgerEntryNumber { get; set; }

        [Key]
        [Required]
        public DateTime LedgerEntryDate { get; set; }

        [Key]
        [Required]
        public Guid JournalHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public decimal TotalDebit { get; set; }

        [Required]
        public decimal TotalCredit { get; set; }

        [Required]
        public double TotalDebitQty { get; set; }

        [Required]
        public double TotalCreditQty { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

    }
}
