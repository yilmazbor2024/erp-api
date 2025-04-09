using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdAllocationTemplate")]
    public partial class cdAllocationTemplate
    {
        public cdAllocationTemplate()
        {
            cdAllocationTemplateDescs = new HashSet<cdAllocationTemplateDesc>();
            dfPeriodicalAllocationRules = new HashSet<dfPeriodicalAllocationRule>();
            prAllocationTemplateParameterValues = new HashSet<prAllocationTemplateParameterValue>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AllocationTemplateCode { get; set; }

        [Required]
        public DateTime TemplateDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AllocationSourceTypeCode { get; set; }

        [Required]
        public bool IncludeWarehouseInventory { get; set; }

        [Required]
        public bool IncludeRemainingOrder { get; set; }

        [Required]
        public bool IncludeRemainingOrderAsn { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AllocationRuleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        public string ProductFilterString { get; set; }

        public string ProductFilterStringSQL { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ChannelTemplateCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsAllocationRule bsAllocationRule { get; set; }
        public virtual bsAllocationSourceType bsAllocationSourceType { get; set; }

        public virtual ICollection<cdAllocationTemplateDesc> cdAllocationTemplateDescs { get; set; }
        public virtual ICollection<dfPeriodicalAllocationRule> dfPeriodicalAllocationRules { get; set; }
        public virtual ICollection<prAllocationTemplateParameterValue> prAllocationTemplateParameterValues { get; set; }
    }
}
