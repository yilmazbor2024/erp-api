using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCompanyCreditCard")]
    public partial class cdCompanyCreditCard
    {
        public cdCompanyCreditCard()
        {
            cdCompanyCreditCardDescs = new HashSet<cdCompanyCreditCardDesc>();
            prCompanyCreditCardEarnedPointss = new HashSet<prCompanyCreditCardEarnedPoints>();
            prCompanyCreditCardEmployees = new HashSet<prCompanyCreditCardEmployee>();
            prCompanyCreditCardExpenses = new HashSet<prCompanyCreditCardExpense>();
            prCompanyCreditCardUsageFees = new HashSet<prCompanyCreditCardUsageFee>();
            trCreditCardPaymentLines = new HashSet<trCreditCardPaymentLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCreditCardCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

    
        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardNum { get; set; }

        [Required]
        public byte StatementDay { get; set; }

        [Required]
        public byte StatementMonth { get; set; }

        [Required]
        public byte ExpireMonth { get; set; }

        [Required]
        public short ExpireYear { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PointGLAccCode { get; set; }

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
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdCompanyCreditCardDesc> cdCompanyCreditCardDescs { get; set; }
        public virtual ICollection<prCompanyCreditCardEarnedPoints> prCompanyCreditCardEarnedPointss { get; set; }
        public virtual ICollection<prCompanyCreditCardEmployee> prCompanyCreditCardEmployees { get; set; }
        public virtual ICollection<prCompanyCreditCardExpense> prCompanyCreditCardExpenses { get; set; }
        public virtual ICollection<prCompanyCreditCardUsageFee> prCompanyCreditCardUsageFees { get; set; }
        public virtual ICollection<trCreditCardPaymentLine> trCreditCardPaymentLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
    }
}
