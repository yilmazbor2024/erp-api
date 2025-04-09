using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobTitleLevel")]
    public partial class cdJobTitleLevel
    {
        public cdJobTitleLevel()
        {
            cdJobTitles = new HashSet<cdJobTitle>();
            cdJobTitleLevelDescs = new HashSet<cdJobTitleLevelDesc>();
        }

        [Key]
        [Required]
        public byte JobTitleLevelCode { get; set; }

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

        public virtual ICollection<cdJobTitle> cdJobTitles { get; set; }
        public virtual ICollection<cdJobTitleLevelDesc> cdJobTitleLevelDescs { get; set; }
    }
}
