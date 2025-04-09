using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpAirConnUSB_CashIn")]
    public partial class rpAirConnUSB_CashIn
    {
        public rpAirConnUSB_CashIn()
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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string cashier { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string currency { get; set; }

        [Required]
        public decimal sum { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string document_id { get; set; }

        [Required]
        public int document_number { get; set; }

        [Required]
        public int shift_document_number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string short_document_id { get; set; }

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
