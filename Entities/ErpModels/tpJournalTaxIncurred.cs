using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpJournalTaxIncurred")]
    public partial class tpJournalTaxIncurred
    {
        public tpJournalTaxIncurred()
        {
        }

        [Key]
        [Required]
        public Guid JournalHeaderID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public decimal TaxIncurredAmount { get; set; }

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
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

    }
}
