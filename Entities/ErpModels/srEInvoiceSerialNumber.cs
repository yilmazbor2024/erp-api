using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srEInvoiceSerialNumber")]
    public partial class srEInvoiceSerialNumber
    {
        public srEInvoiceSerialNumber()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string EInvoicePrefixCode { get; set; }

        [Key]
        [Required]
        public decimal ValidYear { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public bool IsTaxFreeDefault { get; set; }

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

    }
}
