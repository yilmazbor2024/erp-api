using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSupportResolveType")]
    public partial class cdSupportResolveType
    {
        public cdSupportResolveType()
        {
            cdSupportResolveTypeDescs = new HashSet<cdSupportResolveTypeDesc>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SupportResolveTypeCode { get; set; }

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

        public virtual ICollection<cdSupportResolveTypeDesc> cdSupportResolveTypeDescs { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
    }
}
