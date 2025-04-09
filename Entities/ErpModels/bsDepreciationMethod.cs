using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDepreciationMethod")]
    public partial class bsDepreciationMethod
    {
        public bsDepreciationMethod()
        {
            bsDepreciationMethodDescs = new HashSet<bsDepreciationMethodDesc>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
        }

        [Key]
        [Required]
        public byte DepreciationMethodCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDepreciationMethodDesc> bsDepreciationMethodDescs { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
    }
}
