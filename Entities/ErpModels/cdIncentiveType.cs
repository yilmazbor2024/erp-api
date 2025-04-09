using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdIncentiveType")]
    public partial class cdIncentiveType
    {
        public cdIncentiveType()
        {
            cdIncentiveTypeDescs = new HashSet<cdIncentiveTypeDesc>();
            trIncentiveHeaders = new HashSet<trIncentiveHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IncentiveTypeCode { get; set; }

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

        public virtual ICollection<cdIncentiveTypeDesc> cdIncentiveTypeDescs { get; set; }
        public virtual ICollection<trIncentiveHeader> trIncentiveHeaders { get; set; }
    }
}
