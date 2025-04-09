using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceEInvoiceXML")]
    public partial class tpInvoiceEInvoiceXML
    {
        public tpInvoiceEInvoiceXML()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public object XmlData { get; set; }

        [Required]
        public byte SendStatus { get; set; }

        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object? IncomingMsg { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EmailAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GSMNo { get; set; }

        public string InvoiceURL { get; set; }

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
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
