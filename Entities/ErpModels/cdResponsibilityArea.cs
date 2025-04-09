using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdResponsibilityArea")]
    public partial class cdResponsibilityArea
    {
        public cdResponsibilityArea()
        {
            cdResponsibilityAreaDescs = new HashSet<cdResponsibilityAreaDesc>();
            prCreditSurveyorResponsibilityAreas = new HashSet<prCreditSurveyorResponsibilityArea>();
            prResponsibilityAreaPostalAddresss = new HashSet<prResponsibilityAreaPostalAddress>();
            prRoundsmanResponsibilityAreas = new HashSet<prRoundsmanResponsibilityArea>();
            prWarehouseResponsibilityAreas = new HashSet<prWarehouseResponsibilityArea>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ResponsibilityAreaCode { get; set; }

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

        public virtual ICollection<cdResponsibilityAreaDesc> cdResponsibilityAreaDescs { get; set; }
        public virtual ICollection<prCreditSurveyorResponsibilityArea> prCreditSurveyorResponsibilityAreas { get; set; }
        public virtual ICollection<prResponsibilityAreaPostalAddress> prResponsibilityAreaPostalAddresss { get; set; }
        public virtual ICollection<prRoundsmanResponsibilityArea> prRoundsmanResponsibilityAreas { get; set; }
        public virtual ICollection<prWarehouseResponsibilityArea> prWarehouseResponsibilityAreas { get; set; }
    }
}
