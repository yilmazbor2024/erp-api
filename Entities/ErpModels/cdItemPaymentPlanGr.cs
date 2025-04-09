using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemPaymentPlanGr")]
    public partial class cdItemPaymentPlanGr
    {
        public cdItemPaymentPlanGr()
        {
            cdItems = new HashSet<cdItem>();
            cdItemPaymentPlanGrDescs = new HashSet<cdItemPaymentPlanGrDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prItemPaymentPlanGrAtts = new HashSet<prItemPaymentPlanGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemPaymentPlanGrCode { get; set; }

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

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdItemPaymentPlanGrDesc> cdItemPaymentPlanGrDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prItemPaymentPlanGrAtt> prItemPaymentPlanGrAtts { get; set; }
    }
}
