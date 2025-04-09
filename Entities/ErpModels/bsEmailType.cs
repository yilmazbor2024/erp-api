using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEmailType")]
    public partial class bsEmailType
    {
        public bsEmailType()
        {
            bsEmailTypeDescs = new HashSet<bsEmailTypeDesc>();
            dfCompanyEmailDefaults = new HashSet<dfCompanyEmailDefault>();
        }

        [Key]
        [Required]
        public byte EmailTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEmailTypeDesc> bsEmailTypeDescs { get; set; }
        public virtual ICollection<dfCompanyEmailDefault> dfCompanyEmailDefaults { get; set; }
    }
}
