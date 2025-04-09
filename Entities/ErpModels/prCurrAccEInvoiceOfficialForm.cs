using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccEInvoiceOfficialForm")]
    public partial class prCurrAccEInvoiceOfficialForm
    {
        public prCurrAccEInvoiceOfficialForm()
        {
        }

        [Key]
        [Required]
        public byte InvoiceTypeCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string XsltName { get; set; }

        [Required]
        public object XsltData { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LanguageCode { get; set; }

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
        public virtual bsInvoiceType bsInvoiceType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
