using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCostCenterHierarchy")]
    public partial class prCostCenterHierarchy
    {
        public prCostCenterHierarchy()
        {
            prCostCenterCostDrivers = new HashSet<prCostCenterCostDriver>();
            prItemCostCenterRatess = new HashSet<prItemCostCenterRates>();
            trBankLineCostCenterRatess = new HashSet<trBankLineCostCenterRates>();
            trCashLineCostCenterRatess = new HashSet<trCashLineCostCenterRates>();
            trExpenseAccrualLineCostCenterRatess = new HashSet<trExpenseAccrualLineCostCenterRates>();
            trExpenseSlipLineCostCenterRatess = new HashSet<trExpenseSlipLineCostCenterRates>();
            trInnerLineCostCenterRatess = new HashSet<trInnerLineCostCenterRates>();
            trInvoiceLineCostCenterRatess = new HashSet<trInvoiceLineCostCenterRates>();
            trJournalLineCostCenterRatess = new HashSet<trJournalLineCostCenterRates>();
        }

        [Key]
        [Required]
        public int CostCenterHierarchyID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object HierarchyNode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

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
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<prCostCenterCostDriver> prCostCenterCostDrivers { get; set; }
        public virtual ICollection<prItemCostCenterRates> prItemCostCenterRatess { get; set; }
        public virtual ICollection<trBankLineCostCenterRates> trBankLineCostCenterRatess { get; set; }
        public virtual ICollection<trCashLineCostCenterRates> trCashLineCostCenterRatess { get; set; }
        public virtual ICollection<trExpenseAccrualLineCostCenterRates> trExpenseAccrualLineCostCenterRatess { get; set; }
        public virtual ICollection<trExpenseSlipLineCostCenterRates> trExpenseSlipLineCostCenterRatess { get; set; }
        public virtual ICollection<trInnerLineCostCenterRates> trInnerLineCostCenterRatess { get; set; }
        public virtual ICollection<trInvoiceLineCostCenterRates> trInvoiceLineCostCenterRatess { get; set; }
        public virtual ICollection<trJournalLineCostCenterRates> trJournalLineCostCenterRatess { get; set; }
    }
}
