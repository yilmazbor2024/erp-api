using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpExpenseInvoiceConfirmation")]
    public partial class tpExpenseInvoiceConfirmation
    {
        public tpExpenseInvoiceConfirmation()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        public Guid? ExpenseInvoiceConfirmationRuleID { get; set; }

        public Guid? CompanyExpenseInvoiceConfirmationRuleID { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public bool IsRejected { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RejectReason { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

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
        public virtual prCompanyExpenseInvoiceConfirmationRule prCompanyExpenseInvoiceConfirmationRule { get; set; }
        public virtual prExpenseInvoiceConfirmationRule prExpenseInvoiceConfirmationRule { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
