using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBankHeaderOnlineBankIntegration")]
    public partial class tpBankHeaderOnlineBankIntegration
    {
        public tpBankHeaderOnlineBankIntegration()
        {
        }

        [Key]
        [Required]
        public Guid BankHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OnlineBankPaymentID { get; set; }

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
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineBankWebServiceCode { get; set; }

        // Navigation Properties
        public virtual trBankHeader trBankHeader { get; set; }
        public virtual cdOnlineBankWebService cdOnlineBankWebService { get; set; }

    }
}
