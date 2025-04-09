using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrAccLotGr")]
    public partial class cdCurrAccLotGr
    {
        public cdCurrAccLotGr()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCurrAccLotGrDescs = new HashSet<cdCurrAccLotGrDesc>();
            prCurrAccLotGrAtts = new HashSet<prCurrAccLotGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccLotGrCode { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCurrAccLotGrDesc> cdCurrAccLotGrDescs { get; set; }
        public virtual ICollection<prCurrAccLotGrAtt> prCurrAccLotGrAtts { get; set; }
    }
}
