using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductCollectionGr")]
    public partial class cdProductCollectionGr
    {
        public cdProductCollectionGr()
        {
            cdItems = new HashSet<cdItem>();
        }

        [Key]
        [Required]
        public int ProductCollectionGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SeasonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CollectionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StoryBoardCode { get; set; }

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
        public virtual cdSeason cdSeason { get; set; }
        public virtual cdStoryBoard cdStoryBoard { get; set; }
        public virtual cdCollection cdCollection { get; set; }

        public virtual ICollection<cdItem> cdItems { get; set; }
    }
}
