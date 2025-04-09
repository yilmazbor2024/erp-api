using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBatchGroup")]
    public partial class cdBatchGroup
    {
        public cdBatchGroup()
        {
            cdBatchGroupDescs = new HashSet<cdBatchGroupDesc>();
            cdRolls = new HashSet<cdRoll>();
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemRollNumberPickings = new HashSet<stItemRollNumberPicking>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchGroupCode { get; set; }

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

        public virtual ICollection<cdBatchGroupDesc> cdBatchGroupDescs { get; set; }
        public virtual ICollection<cdRoll> cdRolls { get; set; }
        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemRollNumberPicking> stItemRollNumberPickings { get; set; }
    }
}
