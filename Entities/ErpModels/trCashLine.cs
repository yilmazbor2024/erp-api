using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCashLine")]
    public partial class trCashLine
    {
        public trCashLine()
        {
            tpCashFTAttributes = new HashSet<tpCashFTAttribute>();
            trCashLineCostCenterRatess = new HashSet<trCashLineCostCenterRates>();
            trCashLineCurrencys = new HashSet<trCashLineCurrency>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid CashLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public byte? CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        public string LineDescription { get; set; }

        [Required]
        public byte EmployeePayTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccCurrencyCode { get; set; }

        [Required]
        public double CurrAccExchangeRate { get; set; }

        [Required]
        public decimal CurrAccAmount { get; set; }

        [Required]
        public Guid CashHeaderID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsEmployeePayType bsEmployeePayType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual trCashHeader trCashHeader { get; set; }

        public virtual ICollection<tpCashFTAttribute> tpCashFTAttributes { get; set; }
        public virtual ICollection<trCashLineCostCenterRates> trCashLineCostCenterRatess { get; set; }
        public virtual ICollection<trCashLineCurrency> trCashLineCurrencys { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
