using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsChannelType")]
    public partial class bsChannelType
    {
        public bsChannelType()
        {
            bsChannelTypeDescs = new HashSet<bsChannelTypeDesc>();
            prChannelTemplateCurrAccs = new HashSet<prChannelTemplateCurrAcc>();
        }

        [Key]
        [Required]
        public byte ChannelTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsChannelTypeDesc> bsChannelTypeDescs { get; set; }
        public virtual ICollection<prChannelTemplateCurrAcc> prChannelTemplateCurrAccs { get; set; }
    }
}
