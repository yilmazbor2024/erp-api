using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpEArchiveIntegratorControl")]
    public partial class rpEArchiveIntegratorControl
    {
        public rpEArchiveIntegratorControl()
        {
        }

        [Key]
        [Required]
        public Guid EArchiveIntegratorControlID { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public TimeSpan InvoiceTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EInvoiceNumber { get; set; }

        [Required]
        public decimal TaxBase { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

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

    }
}
