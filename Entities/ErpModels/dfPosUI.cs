using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPosUI")]
    public partial class dfPosUI
    {
        public dfPosUI()
        {
            dfPosUIDescs = new HashSet<dfPosUIDesc>();
            dfPosUISettings = new HashSet<dfPosUISetting>();
            dfUserPosUISettingss = new HashSet<dfUserPosUISettings>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PosUICode { get; set; }

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
        public virtual dfStoreDefault dfStoreDefault { get; set; }

        public virtual ICollection<dfPosUIDesc> dfPosUIDescs { get; set; }
        public virtual ICollection<dfPosUISetting> dfPosUISettings { get; set; }
        public virtual ICollection<dfUserPosUISettings> dfUserPosUISettingss { get; set; }
    }
}
