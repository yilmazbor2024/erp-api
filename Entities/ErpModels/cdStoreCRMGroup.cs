using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStoreCRMGroup")]
    public partial class cdStoreCRMGroup
    {
        public cdStoreCRMGroup()
        {
            cdPresentCardTypes = new HashSet<cdPresentCardType>();
            cdStoreCRMGroupDescs = new HashSet<cdStoreCRMGroupDesc>();
            prCurrAccUserWarnings = new HashSet<prCurrAccUserWarning>();
            prStorePropertiess = new HashSet<prStoreProperties>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StoreCRMGroupCode { get; set; }

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

        public virtual ICollection<cdPresentCardType> cdPresentCardTypes { get; set; }
        public virtual ICollection<cdStoreCRMGroupDesc> cdStoreCRMGroupDescs { get; set; }
        public virtual ICollection<prCurrAccUserWarning> prCurrAccUserWarnings { get; set; }
        public virtual ICollection<prStoreProperties> prStorePropertiess { get; set; }
    }
}
