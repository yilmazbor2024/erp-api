using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfEInvoiceWebServiceParameters")]
    public partial class dfEInvoiceWebServiceParameters
    {
        public dfEInvoiceWebServiceParameters()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }
 

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EInvoiceWebServiceCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ParameterName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ParameterValue { get; set; }

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

    }
}
