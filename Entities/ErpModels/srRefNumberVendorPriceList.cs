using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberVendorPriceList")]
    public partial class srRefNumberVendorPriceList
    {
        public srRefNumberVendorPriceList()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public decimal VendorPriceListNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

    }
}
