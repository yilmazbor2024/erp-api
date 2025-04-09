using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPayType")]
    public partial class bsPayType
    {
        public bsPayType()
        {
            bsPayTypeDescs = new HashSet<bsPayTypeDesc>();
            hrEmployeeWages = new HashSet<hrEmployeeWage>();
        }

        [Key]
        [Required]
        public byte PayTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPayTypeDesc> bsPayTypeDescs { get; set; }
        public virtual ICollection<hrEmployeeWage> hrEmployeeWages { get; set; }
    }
}
