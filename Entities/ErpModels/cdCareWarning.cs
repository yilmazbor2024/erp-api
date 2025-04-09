using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCareWarning")]
    public partial class cdCareWarning
    {
        public cdCareWarning()
        {
            cdCareWarningDescs = new HashSet<cdCareWarningDesc>();
            prCareWarningTemplateAtts = new HashSet<prCareWarningTemplateAtt>();
            prProductCareWarnings = new HashSet<prProductCareWarning>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CareWarningCode { get; set; }

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

        public virtual ICollection<cdCareWarningDesc> cdCareWarningDescs { get; set; }
        public virtual ICollection<prCareWarningTemplateAtt> prCareWarningTemplateAtts { get; set; }
        public virtual ICollection<prProductCareWarning> prProductCareWarnings { get; set; }
    }
}
