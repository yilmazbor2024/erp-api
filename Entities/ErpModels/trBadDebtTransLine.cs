using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransLine")]
    public partial class trBadDebtTransLine
    {
        public trBadDebtTransLine()
        {
            trBadDebtTransLineAddExpenses = new HashSet<trBadDebtTransLineAddExpense>();
            trBadDebtTransLineInstalments = new HashSet<trBadDebtTransLineInstalment>();
            trBadDebtTransLineResults = new HashSet<trBadDebtTransLineResult>();
        }

        [Key]
        [Required]
        public Guid BadDebtTransLineID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public byte DebtStatusTypeCode { get; set; }

        [Required]
        public bool DenyRetailSale { get; set; }

        [Required]
        public bool DenyInstalmentSale { get; set; }

        [Required]
        public bool DenyReturnRetailSale { get; set; }

        [Required]
        public bool DenyReturnInstalmentSale { get; set; }

        [Required]
        public bool DenyInstalmentPayment { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExecutionOfficeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public decimal ExecutionExpense { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BadDebtReasonCode { get; set; }

        [Required]
        public bool DenyGuarantor { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public Guid BadDebtTransHeaderID { get; set; }

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
        public virtual cdBadDebtReason cdBadDebtReason { get; set; }
        public virtual trBadDebtTransHeader trBadDebtTransHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trBadDebtTransLineAddExpense> trBadDebtTransLineAddExpenses { get; set; }
        public virtual ICollection<trBadDebtTransLineInstalment> trBadDebtTransLineInstalments { get; set; }
        public virtual ICollection<trBadDebtTransLineResult> trBadDebtTransLineResults { get; set; }
    }
}
