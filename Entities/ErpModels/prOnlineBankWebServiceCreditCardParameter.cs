using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prOnlineBankWebServiceCreditCardParameter")]
    public partial class prOnlineBankWebServiceCreditCardParameter
    {
        public prOnlineBankWebServiceCreditCardParameter()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VPosBankMerchantID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineBankWebServiceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

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
        public virtual cdOnlineBankWebService cdOnlineBankWebService { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }

    }
}
