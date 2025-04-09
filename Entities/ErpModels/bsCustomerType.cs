using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCustomerType")]
    public partial class bsCustomerType
    {
        public bsCustomerType()
        {
            bsCustomerTypeDescs = new HashSet<bsCustomerTypeDesc>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
        }

        [Key]
        [Required]
        public byte CustomerTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCustomerTypeDesc> bsCustomerTypeDescs { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
    }
}
