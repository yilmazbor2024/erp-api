using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreFolder")]
    public partial class dfStoreFolder
    {
        public dfStoreFolder()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FolderCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FolderPath { get; set; }

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
        public virtual bsFolder bsFolder { get; set; }

    }
}
