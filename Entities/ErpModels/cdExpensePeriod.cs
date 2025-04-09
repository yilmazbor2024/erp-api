using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExpensePeriod")]
    public partial class cdExpensePeriod
    {
        public cdExpensePeriod()
        {
            cdExpensePeriodDescs = new HashSet<cdExpensePeriodDesc>();
            prCostCenterCostDrivers = new HashSet<prCostCenterCostDriver>();
            prItemCostCenterRatess = new HashSet<prItemCostCenterRates>();
            trCostCenterDistributionss = new HashSet<trCostCenterDistributions>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExpensePeriodCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdExpensePeriodDesc> cdExpensePeriodDescs { get; set; }
        public virtual ICollection<prCostCenterCostDriver> prCostCenterCostDrivers { get; set; }
        public virtual ICollection<prItemCostCenterRates> prItemCostCenterRatess { get; set; }
        public virtual ICollection<trCostCenterDistributions> trCostCenterDistributionss { get; set; }
    }
}
