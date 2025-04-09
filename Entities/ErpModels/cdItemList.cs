using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemList")]
    public partial class cdItemList
    {
        public cdItemList()
        {
            cdItemListDescs = new HashSet<cdItemListDesc>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
            prItemListContents = new HashSet<prItemListContent>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemListCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdItemListDesc> cdItemListDescs { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
        public virtual ICollection<prItemListContent> prItemListContents { get; set; }
    }
}
