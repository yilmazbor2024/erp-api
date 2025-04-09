using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCareWarningTemplate")]
    public partial class cdCareWarningTemplate
    {
        public cdCareWarningTemplate()
        {
            cdCareWarningTemplateDescs = new HashSet<cdCareWarningTemplateDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prCareWarningTemplateAtts = new HashSet<prCareWarningTemplateAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CareWarningTemplateCode { get; set; }

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

        public virtual ICollection<cdCareWarningTemplateDesc> cdCareWarningTemplateDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prCareWarningTemplateAtt> prCareWarningTemplateAtts { get; set; }
    }
}
