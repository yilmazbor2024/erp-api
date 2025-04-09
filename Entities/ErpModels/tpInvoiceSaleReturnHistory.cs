using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceSaleReturnHistory")]
    public partial class tpInvoiceSaleReturnHistory
    {
        public tpInvoiceSaleReturnHistory()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceSaleReturnHistoryID { get; set; }

        [Required]
        public Guid BaseSaleHeaderID { get; set; }

        [Required]
        public Guid BaseSaleLineID { get; set; }

        [Required]
        public Guid ReturnSaleHeaderID { get; set; }

        [Required]
        public Guid ReturnSaleLineID { get; set; }

        public Guid? SaleHeaderID { get; set; }

        public Guid? SaleLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PosActionType { get; set; }

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
