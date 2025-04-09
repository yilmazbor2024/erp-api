using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdMilitaryServiceStatus")]
    public partial class cdMilitaryServiceStatus
    {
        public cdMilitaryServiceStatus()
        {
            cdMilitaryServiceStatusDescs = new HashSet<cdMilitaryServiceStatusDesc>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MilitaryServiceStatusCode { get; set; }

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

        public virtual ICollection<cdMilitaryServiceStatusDesc> cdMilitaryServiceStatusDescs { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
    }
}
