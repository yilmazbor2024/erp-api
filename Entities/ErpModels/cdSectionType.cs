using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSectionType")]
    public partial class cdSectionType
    {
        public cdSectionType()
        {
            cdSectionTypeDescs = new HashSet<cdSectionTypeDesc>();
            prSections = new HashSet<prSection>();
        }

        [Key]
        [Required]
        public short SectionTypeCode { get; set; }

        [Required]
        public bool IsGoodAcceptanceArea { get; set; }

        [Required]
        public bool IsGoodCollectionArea { get; set; }

        [Required]
        public bool PermitInTransaction { get; set; }

        [Required]
        public bool PermitOutTransaction { get; set; }

        [Required]
        public bool PermitSectionTransferTransaction { get; set; }

        [Required]
        public bool PermitTransactionFromGoodAcceptanceArea { get; set; }

        [Required]
        public bool PermitTransactionToGoodCollectionArea { get; set; }

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

        public virtual ICollection<cdSectionTypeDesc> cdSectionTypeDescs { get; set; }
        public virtual ICollection<prSection> prSections { get; set; }
    }
}
