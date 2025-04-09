using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxPaymentType")]
    public partial class bsTaxPaymentType
    {
        public bsTaxPaymentType()
        {
            bsTaxPaymentTypeDescs = new HashSet<bsTaxPaymentTypeDesc>();
            cdGLAccs = new HashSet<cdGLAcc>();
        }

        [Key]
        [Required]
        public byte TaxPaymentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTaxPaymentTypeDesc> bsTaxPaymentTypeDescs { get; set; }
        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
    }
}
