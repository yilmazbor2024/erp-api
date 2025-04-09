using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCashHeader")]
    public partial class trCashHeader
    {
        public trCashHeader()
        {
            tpCashATAttributes = new HashSet<tpCashATAttribute>();
            trCashLines = new HashSet<trCashLine>();
        }

        [Key]
        [Required]
        public Guid CashHeaderID { get; set; }

        [Required]
        public byte CashTransTypeCode { get; set; }

        [Required]
        public object CashTransNumber { get; set; }

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

        public byte? CashCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashCurrAccCode { get; set; }

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
        public bool IsForeignCurrencyTransaction { get; set; }

        [Required]
        public bool IsDiffCurrencyEachLine { get; set; }

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

        [Required]
        public bool IsPostingApproved { get; set; }

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
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual bsCashTransType bsCashTransType { get; set; }

        public virtual ICollection<tpCashATAttribute> tpCashATAttributes { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
    }
}
