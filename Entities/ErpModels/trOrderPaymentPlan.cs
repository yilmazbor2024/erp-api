using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderPaymentPlan")]
    public partial class trOrderPaymentPlan
    {
        public trOrderPaymentPlan()
        {
            tpInnerLineDocuments = new HashSet<tpInnerLineDocument>();
            trBadDebtTransLineInstalments = new HashSet<trBadDebtTransLineInstalment>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid OrderPaymentPlanID { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal PaidAmount { get; set; }

        [Required]
        public decimal CanceledAmount { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        [Required]
        public bool IsClosed { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }

        public virtual ICollection<tpInnerLineDocument> tpInnerLineDocuments { get; set; }
        public virtual ICollection<trBadDebtTransLineInstalment> trBadDebtTransLineInstalments { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
