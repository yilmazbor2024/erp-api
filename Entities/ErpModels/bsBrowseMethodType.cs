using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBrowseMethodType")]
    public partial class bsBrowseMethodType
    {
        public bsBrowseMethodType()
        {
            bsBrowseMethodTypeDescs = new HashSet<bsBrowseMethodTypeDesc>();
        }

        [Key]
        [Required]
        public byte BrowseMethodTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBrowseMethodTypeDesc> bsBrowseMethodTypeDescs { get; set; }
    }
}
