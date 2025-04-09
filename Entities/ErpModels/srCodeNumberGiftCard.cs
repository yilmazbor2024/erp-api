using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srCodeNumberGiftCard")]
    public partial class srCodeNumberGiftCard
    {
        public srCodeNumberGiftCard()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Key]
        [Required]
        public decimal StoreCodeNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdItem cdItem { get; set; }

    }
}
