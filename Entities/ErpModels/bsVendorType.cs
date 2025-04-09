using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsVendorType")]
    public partial class bsVendorType
    {
        public bsVendorType()
        {
            bsVendorTypeDescs = new HashSet<bsVendorTypeDesc>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
        }

        [Key]
        [Required]
        public byte VendorTypeCode { get; set; }

        [Required]
        public bool UseAgentPerformance { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsVendorTypeDesc> bsVendorTypeDescs { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
    }
}
