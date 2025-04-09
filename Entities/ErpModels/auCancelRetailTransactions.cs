using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCancelRetailTransactions")]
    public partial class auCancelRetailTransactions
    {
        public auCancelRetailTransactions()
        {
        }

        [Key]
        [Required]
        public Guid CancelRetailTransactionsID { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public Guid HeaderID { get; set; }

        public Guid? LineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TransactionCancelReasonCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RefNumber { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public decimal TaxBase { get; set; }

        [Required]
        public decimal PCT { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

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
