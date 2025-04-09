using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInnerLineDocument")]
    public partial class tpInnerLineDocument
    {
        public tpInnerLineDocument()
        {
        }

        [Key]
        [Required]
        public Guid InnerLineDocumentID { get; set; }

        [Required]
        public Guid InnerLineID { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? DebitLineID { get; set; }

        public Guid? OrderHeaderID { get; set; }

        public Guid? OrderPaymentPlanID { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentSerialNumber { get; set; }

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
        public virtual trInnerLine trInnerLine { get; set; }
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trOrderPaymentPlan trOrderPaymentPlan { get; set; }

    }
}
