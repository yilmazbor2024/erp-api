using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpDeletedFiscalInvoice")]
    public partial class tpDeletedFiscalInvoice
    {
        public tpDeletedFiscalInvoice()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InvoiceNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public TimeSpan InvoiceTime { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte FormType { get; set; }

        [Required]
        public bool IsSalesViaInternet { get; set; }

        [Required]
        public decimal TaxBase { get; set; }

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
