using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEInvoiceWebServiceOffice")]
    public partial class prEInvoiceWebServiceOffice
    {
        public prEInvoiceWebServiceOffice()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EInvoiceWebServiceCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

    

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EInvoiceBranchCode { get; set; }

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
        public virtual cdEInvoiceWebService cdEInvoiceWebService { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }

    }
}
