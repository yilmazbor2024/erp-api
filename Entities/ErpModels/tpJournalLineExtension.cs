using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpJournalLineExtension")]
    public partial class tpJournalLineExtension
    {
        public tpJournalLineExtension()
        {
        }

        [Key]
        [Required]
        public Guid JournalLineID { get; set; }

        [Required]
        public byte InvoiceTypeCode { get; set; }

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
        public virtual bsInvoiceType bsInvoiceType { get; set; }

    }
}
