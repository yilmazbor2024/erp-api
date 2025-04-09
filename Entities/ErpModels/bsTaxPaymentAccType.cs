using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxPaymentAccType")]
    public partial class bsTaxPaymentAccType
    {
        public bsTaxPaymentAccType()
        {
            bsTaxPaymentAccTypeDescs = new HashSet<bsTaxPaymentAccTypeDesc>();
            cdGLAccs = new HashSet<cdGLAcc>();
        }

        [Key]
        [Required]
        public byte TaxPaymentAccTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTaxPaymentAccTypeDesc> bsTaxPaymentAccTypeDescs { get; set; }
        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
    }
}
