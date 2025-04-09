using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCustomsProductGroup")]
    public partial class bsCustomsProductGroup
    {
        public bsCustomsProductGroup()
        {
            bsCustomsProductGroupDescs = new HashSet<bsCustomsProductGroupDesc>();
            cdItems = new HashSet<cdItem>();
        }

        [Key]
        [Required]
        public byte CustomsProductGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        public bool IsLimitedSaleProduct { get; set; }

        [Required]
        public double DepartureStoreLimit { get; set; }

        [Required]
        public double ArrivalStoreLimit { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }

        public virtual ICollection<bsCustomsProductGroupDesc> bsCustomsProductGroupDescs { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
    }
}
