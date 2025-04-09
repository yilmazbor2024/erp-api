using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBasePrice")]
    public partial class bsBasePrice
    {
        public bsBasePrice()
        {
            auBasePricePermits = new HashSet<auBasePricePermit>();
            bsBasePriceDescs = new HashSet<bsBasePriceDesc>();
            cdLabelTypes = new HashSet<cdLabelType>();
            prItemBasePrices = new HashSet<prItemBasePrice>();
        }

        [Key]
        [Required]
        public byte BasePriceCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auBasePricePermit> auBasePricePermits { get; set; }
        public virtual ICollection<bsBasePriceDesc> bsBasePriceDescs { get; set; }
        public virtual ICollection<cdLabelType> cdLabelTypes { get; set; }
        public virtual ICollection<prItemBasePrice> prItemBasePrices { get; set; }
    }
}
