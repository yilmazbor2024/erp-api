using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceLineExpenseAccrual")]
    public partial class tpInvoiceLineExpenseAccrual
    {
        public tpInvoiceLineExpenseAccrual()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public Guid ExpenseAccrualHeaderID { get; set; }

        public Guid? ExpenseAccrualLineID { get; set; }

        [Required]
        public bool CloseWithExpenseAccrual { get; set; }

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
        public virtual trExpenseAccrualLine trExpenseAccrualLine { get; set; }
        public virtual trExpenseAccrualHeader trExpenseAccrualHeader { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
