using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpAirConnUSB_XReport")]
    public partial class rpAirConnUSB_XReport
    {
        public rpAirConnUSB_XReport()
        {
        }

        [Key]
        [Required]
        public Guid TransactionID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte PosTerminalID { get; set; }

        [Required]
        public DateTime createdAtUtc { get; set; }

        [Required]
        public int docCountToSend { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string document_id { get; set; }

        [Required]
        public int firstDocNumber { get; set; }

        [Required]
        public int lastDocNumber { get; set; }

        [Required]
        public int reportNumber { get; set; }

        [Required]
        public DateTime shiftCloseAtUtc { get; set; }

        [Required]
        public DateTime shiftOpenAtUtc { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string currency { get; set; }

        [Required]
        public decimal correctionBonusSum { get; set; }

        [Required]
        public decimal correctionCashSum { get; set; }

        [Required]
        public decimal correctionCashlessSum { get; set; }

        [Required]
        public int correctionCount { get; set; }

        [Required]
        public decimal correctionCreditSum { get; set; }

        [Required]
        public decimal correctionPrepaymentSum { get; set; }

        [Required]
        public decimal correctionSum { get; set; }

        [Required]
        public int correctionVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal correctionVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int correctionVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal correctionVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int correctionVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal correctionVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int correctionVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal correctionVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int correctionVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal correctionVatAmounts_vatSum_5 { get; set; }

        [Required]
        public decimal creditpayBonusSum { get; set; }

        [Required]
        public decimal creditpayCashSum { get; set; }

        [Required]
        public decimal creditpayCashlessSum { get; set; }

        [Required]
        public int creditpayCount { get; set; }

        [Required]
        public decimal creditpayCreditSum { get; set; }

        [Required]
        public decimal creditpayPrepaymentSum { get; set; }

        [Required]
        public decimal creditpaySum { get; set; }

        [Required]
        public int creditpayVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal creditpayVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int creditpayVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal creditpayVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int creditpayVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal creditpayVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int creditpayVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal creditpayVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int creditpayVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal creditpayVatAmounts_vatSum_5 { get; set; }

        [Required]
        public int depositCount { get; set; }

        [Required]
        public decimal depositSum { get; set; }

        [Required]
        public decimal moneyBackBonusSum { get; set; }

        [Required]
        public decimal moneyBackCashSum { get; set; }

        [Required]
        public decimal moneyBackCashlessSum { get; set; }

        [Required]
        public int moneyBackCount { get; set; }

        [Required]
        public decimal moneyBackCreditSum { get; set; }

        [Required]
        public decimal moneyBackPrepaymentSum { get; set; }

        [Required]
        public decimal moneyBackSum { get; set; }

        [Required]
        public int moneyBackVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal moneyBackVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int moneyBackVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal moneyBackVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int moneyBackVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal moneyBackVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int moneyBackVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal moneyBackVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int moneyBackVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal moneyBackVatAmounts_vatSum_5 { get; set; }

        [Required]
        public decimal prepayBonusSum { get; set; }

        [Required]
        public decimal prepayCashSum { get; set; }

        [Required]
        public decimal prepayCashlessSum { get; set; }

        [Required]
        public int prepayCount { get; set; }

        [Required]
        public decimal prepayCreditSum { get; set; }

        [Required]
        public decimal prepayPrepaymentSum { get; set; }

        [Required]
        public decimal prepaySum { get; set; }

        [Required]
        public int prepayVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal prepayVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int prepayVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal prepayVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int prepayVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal prepayVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int prepayVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal prepayVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int prepayVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal prepayVatAmounts_vatSum_5 { get; set; }

        [Required]
        public decimal rollbackBonusSum { get; set; }

        [Required]
        public decimal rollbackCashSum { get; set; }

        [Required]
        public decimal rollbackCashlessSum { get; set; }

        [Required]
        public int rollbackCount { get; set; }

        [Required]
        public decimal rollbackCreditSum { get; set; }

        [Required]
        public decimal rollbackPrepaymentSum { get; set; }

        [Required]
        public decimal rollbackSum { get; set; }

        [Required]
        public int rollbackVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal rollbackVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int rollbackVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal rollbackVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int rollbackVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal rollbackVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int rollbackVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal rollbackVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int rollbackVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal rollbackVatAmounts_vatSum_5 { get; set; }

        [Required]
        public decimal saleBonusSum { get; set; }

        [Required]
        public decimal saleCashSum { get; set; }

        [Required]
        public decimal saleCashlessSum { get; set; }

        [Required]
        public int saleCount { get; set; }

        [Required]
        public decimal saleCreditSum { get; set; }

        [Required]
        public decimal salePrepaymentSum { get; set; }

        [Required]
        public decimal saleSum { get; set; }

        [Required]
        public int saleVatAmounts_vatPercent_1 { get; set; }

        [Required]
        public decimal saleVatAmounts_vatSum_1 { get; set; }

        [Required]
        public int saleVatAmounts_vatPercent_2 { get; set; }

        [Required]
        public decimal saleVatAmounts_vatSum_2 { get; set; }

        [Required]
        public int saleVatAmounts_vatPercent_3 { get; set; }

        [Required]
        public decimal saleVatAmounts_vatSum_3 { get; set; }

        [Required]
        public int saleVatAmounts_vatPercent_4 { get; set; }

        [Required]
        public decimal saleVatAmounts_vatSum_4 { get; set; }

        [Required]
        public int saleVatAmounts_vatPercent_5 { get; set; }

        [Required]
        public decimal saleVatAmounts_vatSum_5 { get; set; }

        [Required]
        public int withdrawCount { get; set; }

        [Required]
        public decimal withdrawSum { get; set; }

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
