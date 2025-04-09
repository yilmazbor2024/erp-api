using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpOnlineBankFuturePosIncomeByReturnDate")]
    public partial class rpOnlineBankFuturePosIncomeByReturnDate
    {
        public rpOnlineBankFuturePosIncomeByReturnDate()
        {
        }

        [Key]
        [Required]
        public Guid OnlineBankFuturePosIncomeByReturnDateID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PosPaymentID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AccountID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object AccountName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TaxNumber { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public DateTime ValorDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal GrossAmount { get; set; }

        [Required]
        public decimal TotalCommission { get; set; }

        [Required]
        public double CommissionRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal BankServiceCommission { get; set; }

        [Required]
        public decimal OtherCommission { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string BusinessNo { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string TerminalID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AuthCode { get; set; }

        [Required]
        public int InstallmentNumber { get; set; }

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

    }
}
