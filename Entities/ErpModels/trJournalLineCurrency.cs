using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalLineCurrency")]
    public partial class trJournalLineCurrency
    {
        public trJournalLineCurrency()
        {
        }

        [Key]
        [Required]
        public Guid JournalLineID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelationCurrencyCode { get; set; }

        [Required]
        public decimal Debit { get; set; }

        [Required]
        public decimal Credit { get; set; }

        [Required]
        public decimal TaxAmount { get; set; }

        [Required]
        public decimal TaxAssessment { get; set; }

        [Required]
        public decimal VatDeducation { get; set; }

        public decimal? TaxAssessmentForDeclaration { get; set; }

        public decimal? TaxAmountForDeclaration { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trJournalLine trJournalLine { get; set; }

    }
}
