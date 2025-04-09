using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auAirConnTransactionInfo")]
    public partial class auAirConnTransactionInfo
    {
        public auAirConnTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid V3TransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DocumentNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ShiftDocumentNumber { get; set; }

        [Required]
        public DateTime ShiftOpenTime { get; set; }

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
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

    }
}
