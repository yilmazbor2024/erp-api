using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEInvoiceWebService")]
    public partial class cdEInvoiceWebService
    {
        public cdEInvoiceWebService()
        {
            cdEInvoiceWebServiceDescs = new HashSet<cdEInvoiceWebServiceDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfEInvoiceWebServiceParameterss = new HashSet<dfEInvoiceWebServiceParameters>();
            prEInvoiceWebServiceCompanys = new HashSet<prEInvoiceWebServiceCompany>();
            prEInvoiceWebServiceOffices = new HashSet<prEInvoiceWebServiceOffice>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EInvoiceWebServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress2 { get; set; }

        [Required]
        public TimeSpan ReceiveCustomerPeriod { get; set; }

        [Required]
        public TimeSpan SendInvoicePeriod { get; set; }

        [Required]
        public TimeSpan ReceiveInvoicePeriod { get; set; }

        [Required]
        public TimeSpan AskInboxStatusPeriod { get; set; }

        [Required]
        public TimeSpan AskOutboxStatusPeriod { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdEInvoiceWebServiceDesc> cdEInvoiceWebServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfEInvoiceWebServiceParameters> dfEInvoiceWebServiceParameterss { get; set; }
        public virtual ICollection<prEInvoiceWebServiceCompany> prEInvoiceWebServiceCompanys { get; set; }
        public virtual ICollection<prEInvoiceWebServiceOffice> prEInvoiceWebServiceOffices { get; set; }
    }
}
