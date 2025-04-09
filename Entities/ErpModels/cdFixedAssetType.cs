using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFixedAssetType")]
    public partial class cdFixedAssetType
    {
        public cdFixedAssetType()
        {
            cdFixedAssetTypeDescs = new HashSet<cdFixedAssetTypeDesc>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
        }

        [Key]
        [Required]
        public byte FixedAssetTypeCode { get; set; }

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

        public virtual ICollection<cdFixedAssetTypeDesc> cdFixedAssetTypeDescs { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
    }
}
