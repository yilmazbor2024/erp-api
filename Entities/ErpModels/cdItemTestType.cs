using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemTestType")]
    public partial class cdItemTestType
    {
        public cdItemTestType()
        {
            cdItemTestTypeDescs = new HashSet<cdItemTestTypeDesc>();
            trItemTestLines = new HashSet<trItemTestLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTestTypeCode { get; set; }

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

        public virtual ICollection<cdItemTestTypeDesc> cdItemTestTypeDescs { get; set; }
        public virtual ICollection<trItemTestLine> trItemTestLines { get; set; }
    }
}
