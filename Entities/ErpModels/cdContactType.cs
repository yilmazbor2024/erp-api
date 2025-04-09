using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdContactType")]
    public partial class cdContactType
    {
        public cdContactType()
        {
            cdContactTypeDescs = new HashSet<cdContactTypeDesc>();
            prCurrAccContacts = new HashSet<prCurrAccContact>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContactTypeCode { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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

        public virtual ICollection<cdContactTypeDesc> cdContactTypeDescs { get; set; }
        public virtual ICollection<prCurrAccContact> prCurrAccContacts { get; set; }
    }
}
