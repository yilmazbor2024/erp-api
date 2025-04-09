using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpWeArePlanetTaxFreeTransaction")]
    public partial class zpWeArePlanetTaxFreeTransaction
    {
        public zpWeArePlanetTaxFreeTransaction()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TaxRefundTagNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public bool IsSentForReturn { get; set; }

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

    }
}
