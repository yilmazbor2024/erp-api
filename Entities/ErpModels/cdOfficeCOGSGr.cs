using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOfficeCOGSGr")]
    public partial class cdOfficeCOGSGr
    {
        public cdOfficeCOGSGr()
        {
            cdOfficeCOGSGrDescs = new HashSet<cdOfficeCOGSGrDesc>();
            prOfficeCOGSGrAtts = new HashSet<prOfficeCOGSGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OfficeCOGSGrCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte COGsCalculationSortOrder { get; set; }

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

        public virtual ICollection<cdOfficeCOGSGrDesc> cdOfficeCOGSGrDescs { get; set; }
        public virtual ICollection<prOfficeCOGSGrAtt> prOfficeCOGSGrAtts { get; set; }
    }
}
