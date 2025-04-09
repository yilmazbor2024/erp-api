using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocation")]
    public partial class trAllocation
    {
        public trAllocation()
        {
            tpAllocationATAttributes = new HashSet<tpAllocationATAttribute>();
            tpAllocationITAttributes = new HashSet<tpAllocationITAttribute>();
            trAllocationChannels = new HashSet<trAllocationChannel>();
            trAllocationParameterValues = new HashSet<trAllocationParameterValue>();
            trAllocationProducts = new HashSet<trAllocationProduct>();
            trAllocationProductQtys = new HashSet<trAllocationProductQty>();
        }

        [Key]
        [Required]
        public Guid AllocationID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object AllocationNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AllocationRuleCode { get; set; }

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
        public string SourceApplicationCode { get; set; }

        public Guid? SourceApplicationID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ParameteredFieldsValue { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsCalculated { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public byte UseRemainingOrderOption { get; set; }

        [Required]
        public bool IsPostingOrder { get; set; }

        [Required]
        public bool IsPostingStoreOrder { get; set; }

        public string ProductFilterStringSQL { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual bsAllocationRule bsAllocationRule { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsAllocationSourceType bsAllocationSourceType { get; set; }

        public virtual ICollection<tpAllocationATAttribute> tpAllocationATAttributes { get; set; }
        public virtual ICollection<tpAllocationITAttribute> tpAllocationITAttributes { get; set; }
        public virtual ICollection<trAllocationChannel> trAllocationChannels { get; set; }
        public virtual ICollection<trAllocationParameterValue> trAllocationParameterValues { get; set; }
        public virtual ICollection<trAllocationProduct> trAllocationProducts { get; set; }
        public virtual ICollection<trAllocationProductQty> trAllocationProductQtys { get; set; }
    }
}
