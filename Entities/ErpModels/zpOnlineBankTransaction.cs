using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpOnlineBankTransaction")]
    public partial class zpOnlineBankTransaction
    {
        public zpOnlineBankTransaction()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OnlineBankPaymentID { get; set; }

        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public bool IsBankDebitted { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [Required]
        public byte AccountTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AccountCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankAccNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IBAN { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelatedBankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankBranchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankAccNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RelatedIBAN { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentExpCode { get; set; }

        [Required]
        public byte Status { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        public byte? BankOpTypeCode { get; set; }

        public byte? ChequeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ChequeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ChequeBankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ChequeBankBranchCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

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
        public bool ModifiedByUser { get; set; }

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
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdBankOpType cdBankOpType { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }

    }
}
