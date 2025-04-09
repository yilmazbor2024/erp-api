using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTsmTransactionInfo")]
    public partial class auTsmTransactionInfo
    {
        public auTsmTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid TsmTransactionInfoID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TsmTransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [Required]
        public byte DocumentType { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IdentityNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CustomerName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public int GetCount { get; set; }

        [Required]
        public bool CancelledFromDevice { get; set; }

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
