using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankHeader")]
    public partial class trBankHeader
    {
        public trBankHeader()
        {
            tpBankATAttributes = new HashSet<tpBankATAttribute>();
            tpBankHeaderOnlineBankIntegrations = new HashSet<tpBankHeaderOnlineBankIntegration>();
            tpBankMT940s = new HashSet<tpBankMT940>();
            trBankLines = new HashSet<trBankLine>();
        }

        [Key]
        [Required]
        public Guid BankHeaderID { get; set; }

        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public object BankTransNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public object RefNumber { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Required]
        public bool SetDocumentTypeAsJournal { get; set; }

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
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual bsBankTransType bsBankTransType { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBankATAttribute> tpBankATAttributes { get; set; }
        public virtual ICollection<tpBankHeaderOnlineBankIntegration> tpBankHeaderOnlineBankIntegrations { get; set; }
        public virtual ICollection<tpBankMT940> tpBankMT940s { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
    }
}
