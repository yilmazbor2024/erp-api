using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOnlineBankWebService")]
    public partial class cdOnlineBankWebService
    {
        public cdOnlineBankWebService()
        {
            cdOnlineBankWebServiceDescs = new HashSet<cdOnlineBankWebServiceDesc>();
            dfOnlineBankWebServiceParameterss = new HashSet<dfOnlineBankWebServiceParameters>();
            prCurrAccOnlineBanks = new HashSet<prCurrAccOnlineBank>();
            prGLAccOnlineBanks = new HashSet<prGLAccOnlineBank>();
            prOnlineBankWebServiceBankInternalParameters = new HashSet<prOnlineBankWebServiceBankInternalParameter>();
            prOnlineBankWebServiceCreditCardParameters = new HashSet<prOnlineBankWebServiceCreditCardParameter>();
            prSubCurrAccOnlineBanks = new HashSet<prSubCurrAccOnlineBank>();
            tpBankHeaderOnlineBankIntegrations = new HashSet<tpBankHeaderOnlineBankIntegration>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineBankWebServiceCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress { get; set; }

        public string ApiKey { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public bool UsePaymentIDAsLineDesription { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddressTNT { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object WebServiceAddressPosrapor { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdOnlineBankWebServiceDesc> cdOnlineBankWebServiceDescs { get; set; }
        public virtual ICollection<dfOnlineBankWebServiceParameters> dfOnlineBankWebServiceParameterss { get; set; }
        public virtual ICollection<prCurrAccOnlineBank> prCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prGLAccOnlineBank> prGLAccOnlineBanks { get; set; }
        public virtual ICollection<prOnlineBankWebServiceBankInternalParameter> prOnlineBankWebServiceBankInternalParameters { get; set; }
        public virtual ICollection<prOnlineBankWebServiceCreditCardParameter> prOnlineBankWebServiceCreditCardParameters { get; set; }
        public virtual ICollection<prSubCurrAccOnlineBank> prSubCurrAccOnlineBanks { get; set; }
        public virtual ICollection<tpBankHeaderOnlineBankIntegration> tpBankHeaderOnlineBankIntegrations { get; set; }
    }
}
