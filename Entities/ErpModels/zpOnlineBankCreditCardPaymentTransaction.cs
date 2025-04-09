using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpOnlineBankCreditCardPaymentTransaction")]
    public partial class zpOnlineBankCreditCardPaymentTransaction
    {
        public zpOnlineBankCreditCardPaymentTransaction()
        {
            tpCreditCardPaymentHeaderOnlineBankIntegrations = new HashSet<tpCreditCardPaymentHeaderOnlineBankIntegration>();
        }

        [Key]
        [Required]
        public Guid OnlineBankTransactionID { get; set; }

        [Required]
        public decimal TotalPaymentAmount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrencyCode { get; set; }

        [Required]
        public int InstallmentNumber { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int TransactionStatusID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransactionStatus { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentExpCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MaskedCreditCardNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PosBankCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CardBankCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AuthCode { get; set; }

        [Required]
        public double ServiceProviderCommissionRate { get; set; }

        [Required]
        public double FirmCommissionRate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Explanation { get; set; }

        [Required]
        public bool IsTransactionCancel { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankTransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VPosBankMerchantID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [Required]
        public object DocumentNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt05 { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LineDescription { get; set; }

        [Required]
        public bool IsProcessed { get; set; }

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
        public decimal SubFirmProgressPayment { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PosBankName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CreditCardHoldersFullName { get; set; }

        // Navigation Properties
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpCreditCardPaymentHeaderOnlineBankIntegration> tpCreditCardPaymentHeaderOnlineBankIntegrations { get; set; }
    }
}
