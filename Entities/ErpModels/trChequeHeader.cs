using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trChequeHeader")]
    public partial class trChequeHeader
    {
        public trChequeHeader()
        {
            tpChequeATAttributes = new HashSet<tpChequeATAttribute>();
            tpInvoiceExchangeDifferencePaidCheques = new HashSet<tpInvoiceExchangeDifferencePaidCheque>();
            trChequeLines = new HashSet<trChequeLine>();
        }

        [Key]
        [Required]
        public Guid ChequeHeaderID { get; set; }

        [Required]
        public byte ChequeTransTypeCode { get; set; }

        [Required]
        public object ChequeTransNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        public byte? CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        public byte? CashCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashCurrAccCode { get; set; }

        public byte? BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual bsChequeTransType bsChequeTransType { get; set; }

        public virtual ICollection<tpChequeATAttribute> tpChequeATAttributes { get; set; }
        public virtual ICollection<tpInvoiceExchangeDifferencePaidCheque> tpInvoiceExchangeDifferencePaidCheques { get; set; }
        public virtual ICollection<trChequeLine> trChequeLines { get; set; }
    }
}
