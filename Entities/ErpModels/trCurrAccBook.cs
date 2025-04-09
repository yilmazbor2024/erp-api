using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCurrAccBook")]
    public partial class trCurrAccBook
    {
        public trCurrAccBook()
        {
            tpCurrAccBookATAttributes = new HashSet<tpCurrAccBookATAttribute>();
            tpCurrAccBookFTAttributes = new HashSet<tpCurrAccBookFTAttribute>();
            trCurrAccBookCurrencys = new HashSet<trCurrAccBookCurrency>();
        }

        [Key]
        [Required]
        public Guid CurrAccBookID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public byte? CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string LineDescription { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [Required]
        public object RefNumber { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        public Guid? ReturnApplicationID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BaseApplicationCode { get; set; }

        public Guid? BaseApplicationID { get; set; }

        [Required]
        public byte DebitTypeCode { get; set; }

        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public byte CashTransTypeCode { get; set; }

        [Required]
        public byte CreditCardPaymentTypeCode { get; set; }

        [Required]
        public byte GiftCardPaymentTypeCode { get; set; }

        [Required]
        public byte ChequeTransTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpCurrAccBookATAttribute> tpCurrAccBookATAttributes { get; set; }
        public virtual ICollection<tpCurrAccBookFTAttribute> tpCurrAccBookFTAttributes { get; set; }
        public virtual ICollection<trCurrAccBookCurrency> trCurrAccBookCurrencys { get; set; }
    }
}
