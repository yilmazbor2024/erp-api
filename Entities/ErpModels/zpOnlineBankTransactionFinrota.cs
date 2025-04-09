using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpOnlineBankTransactionFinrota")]
    public partial class zpOnlineBankTransactionFinrota
    {
        public zpOnlineBankTransactionFinrota()
        {
        }

        [Key]
        [Required]
        public Guid OnlineBankTransactionFinrotaID { get; set; }

        [Required]
        public int Id { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string UniqueId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankAccountErpCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string BankAccountAccountNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string BankAccountBranchNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankAccountSuffix { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankAccountIban { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description1 { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CurrencyCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TcknNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TaxNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Iban { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CurrentAccountCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankAccountCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AccountPlanCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VoucherTypeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string BankEftCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CategoryName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CategoryCode { get; set; }

        [Required]
        public int TransferStatus { get; set; }

        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public byte AccountTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AccountCode { get; set; }

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
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankAccNo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelatedBankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankBranchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankAccNo { get; set; }

        [Required]
        public byte BankOpTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public byte Status { get; set; }

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

    }
}
