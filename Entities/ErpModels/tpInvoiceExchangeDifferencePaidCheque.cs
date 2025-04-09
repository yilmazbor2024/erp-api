using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceExchangeDifferencePaidCheque")]
    public partial class tpInvoiceExchangeDifferencePaidCheque
    {
        public tpInvoiceExchangeDifferencePaidCheque()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Key]
        [Required]
        public Guid ChequeHeaderID { get; set; }

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
        public virtual trChequeHeader trChequeHeader { get; set; }

    }
}
