using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCreditCardType")]
    public partial class cdCreditCardType
    {
        public cdCreditCardType()
        {
            cdCompanyCreditCards = new HashSet<cdCompanyCreditCard>();
            cdCreditCardTypeDescs = new HashSet<cdCreditCardTypeDesc>();
            dfBankPOSReturnsRules = new HashSet<dfBankPOSReturnsRule>();
            dfBulutTahsilatVPosCompanys = new HashSet<dfBulutTahsilatVPosCompany>();
            dfBulutTahsilatVPosOffices = new HashSet<dfBulutTahsilatVPosOffice>();
            dfOfficeCreditCardTypes = new HashSet<dfOfficeCreditCardType>();
            dfPaynetBankIDConverts = new HashSet<dfPaynetBankIDConvert>();
            prCreditCardTypeBINs = new HashSet<prCreditCardTypeBIN>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
            prMarketPlaceCreditCardMappingss = new HashSet<prMarketPlaceCreditCardMappings>();
            prOnlineBankWebServiceCreditCardParameters = new HashSet<prOnlineBankWebServiceCreditCardParameter>();
            prPosTerminalFiscalPrinters = new HashSet<prPosTerminalFiscalPrinter>();
            trCreditCardPaymentLines = new HashSet<trCreditCardPaymentLine>();
            zpOnlineBankCreditCardPaymentTransactions = new HashSet<zpOnlineBankCreditCardPaymentTransaction>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [Required]
        public bool IsCreditCardNumRequired { get; set; }

        [Required]
        public bool IsCreditCardValidCheck { get; set; }

        [Required]
        public byte MaxInstallmentCount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AvailableInstallments { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LikeCashInstallments { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Required]
        public byte BankCardTypeCode { get; set; }

        [Required]
        public bool IsContractedBankCard { get; set; }

        [Required]
        public bool IsDebitCard { get; set; }

        [Required]
        public bool IsOverseasBankCard { get; set; }

        [Required]
        public bool IsCompanyCreditCardType { get; set; }

        [Required]
        public int EditMaskCode { get; set; }

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
        public virtual bsEditMask bsEditMask { get; set; }
        public virtual bsBankCardType bsBankCardType { get; set; }
        public virtual cdBank cdBank { get; set; }

        public virtual ICollection<cdCompanyCreditCard> cdCompanyCreditCards { get; set; }
        public virtual ICollection<cdCreditCardTypeDesc> cdCreditCardTypeDescs { get; set; }
        public virtual ICollection<dfBankPOSReturnsRule> dfBankPOSReturnsRules { get; set; }
        public virtual ICollection<dfBulutTahsilatVPosCompany> dfBulutTahsilatVPosCompanys { get; set; }
        public virtual ICollection<dfBulutTahsilatVPosOffice> dfBulutTahsilatVPosOffices { get; set; }
        public virtual ICollection<dfOfficeCreditCardType> dfOfficeCreditCardTypes { get; set; }
        public virtual ICollection<dfPaynetBankIDConvert> dfPaynetBankIDConverts { get; set; }
        public virtual ICollection<prCreditCardTypeBIN> prCreditCardTypeBINs { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
        public virtual ICollection<prMarketPlaceCreditCardMappings> prMarketPlaceCreditCardMappingss { get; set; }
        public virtual ICollection<prOnlineBankWebServiceCreditCardParameter> prOnlineBankWebServiceCreditCardParameters { get; set; }
        public virtual ICollection<prPosTerminalFiscalPrinter> prPosTerminalFiscalPrinters { get; set; }
        public virtual ICollection<trCreditCardPaymentLine> trCreditCardPaymentLines { get; set; }
        public virtual ICollection<zpOnlineBankCreditCardPaymentTransaction> zpOnlineBankCreditCardPaymentTransactions { get; set; }
    }
}
