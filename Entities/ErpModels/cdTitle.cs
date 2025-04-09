using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTitle")]
    public partial class cdTitle
    {
        public cdTitle()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdTitleDescs = new HashSet<cdTitleDesc>();
            prCurrAccContacts = new HashSet<prCurrAccContact>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TitleCode { get; set; }

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
        public virtual ICollection<cdTitleDesc> cdTitleDescs { get; set; }
        public virtual ICollection<prCurrAccContact> prCurrAccContacts { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
    }
}
