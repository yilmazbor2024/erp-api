using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSeason")]
    public partial class cdSeason
    {
        public cdSeason()
        {
            cdProductCollectionGrs = new HashSet<cdProductCollectionGr>();
            cdSeasonDescs = new HashSet<cdSeasonDesc>();
            cdSubSeasons = new HashSet<cdSubSeason>();
            prItemBasePrices = new HashSet<prItemBasePrice>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SeasonCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ProductCodeParameter { get; set; }

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

        public virtual ICollection<cdProductCollectionGr> cdProductCollectionGrs { get; set; }
        public virtual ICollection<cdSeasonDesc> cdSeasonDescs { get; set; }
        public virtual ICollection<cdSubSeason> cdSubSeasons { get; set; }
        public virtual ICollection<prItemBasePrice> prItemBasePrices { get; set; }
    }
}
