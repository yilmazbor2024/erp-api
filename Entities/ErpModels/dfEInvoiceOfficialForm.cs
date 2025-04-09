using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfEInvoiceOfficialForm")]
    public partial class dfEInvoiceOfficialForm
    {
        public dfEInvoiceOfficialForm()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

        [Key]
        [Required]
        public byte FormType { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string XsltName { get; set; }

        [Required]
        public object XsltData { get; set; }

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
        public virtual bsProcessFlow bsProcessFlow { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdOffice cdOffice { get; set; }

    }
}
