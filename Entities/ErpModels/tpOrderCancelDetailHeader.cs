using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderCancelDetailHeader")]
    public partial class tpOrderCancelDetailHeader
    {
        public tpOrderCancelDetailHeader()
        {
            tpOrderCancelDetails = new HashSet<tpOrderCancelDetail>();
            tpOrderCancelReturnTransactionss = new HashSet<tpOrderCancelReturnTransactions>();
        }

        [Key]
        [Required]
        public Guid OrderCancelDetailHeaderID { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

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
        public short PosTerminalID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpOrderCancelDetail> tpOrderCancelDetails { get; set; }
        public virtual ICollection<tpOrderCancelReturnTransactions> tpOrderCancelReturnTransactionss { get; set; }
    }
}
