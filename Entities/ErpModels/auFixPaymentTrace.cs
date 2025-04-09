using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auFixPaymentTrace")]
    public partial class auFixPaymentTrace
    {
        public auFixPaymentTrace()
        {
        }

        [Required]
        public Guid TraceHeaderID { get; set; }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public bool NewRecord { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InvoiceNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public TimeSpan InvoiceTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal SeriesNumber { get; set; }

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

        [Required]
        public byte CashCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashCurrAccCode { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AcquirerBankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IssuerBankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardNum { get; set; }

        [Required]
        public byte CreditCardInstallmentCount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ChequeCode { get; set; }

        [Required]
        public byte ChequeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Payment { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
