using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsFolder")]
    public partial class bsFolder
    {
        public bsFolder()
        {
            bsFolderDescs = new HashSet<bsFolderDesc>();
            dfCompanyFolders = new HashSet<dfCompanyFolder>();
            dfGlobalFolders = new HashSet<dfGlobalFolder>();
            dfStoreFolders = new HashSet<dfStoreFolder>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FolderCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsFolderDesc> bsFolderDescs { get; set; }
        public virtual ICollection<dfCompanyFolder> dfCompanyFolders { get; set; }
        public virtual ICollection<dfGlobalFolder> dfGlobalFolders { get; set; }
        public virtual ICollection<dfStoreFolder> dfStoreFolders { get; set; }
    }
}
