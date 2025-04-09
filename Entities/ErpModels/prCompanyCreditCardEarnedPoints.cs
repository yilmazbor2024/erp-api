using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCompanyCreditCardEarnedPoints")]
    public partial class prCompanyCreditCardEarnedPoints
    {
        public prCompanyCreditCardEarnedPoints()
        {
        }

        [Key]
        [Required]
        public Guid CompanyCreditCardEarnedPointID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCreditCardCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [Required]
        public object RefNumber { get; set; }

        [Required]
        public decimal Point { get; set; }

        [Required]
        public bool IsPostingToJournal { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? JournalHeaderID { get; set; }

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
        public virtual cdCompanyCreditCard cdCompanyCreditCard { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

    }
}
