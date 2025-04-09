using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemDim3")]
    public partial class cdItemDim3
    {
        public cdItemDim3()
        {
            prItemVariants = new HashSet<prItemVariant>();
            prLotQtys = new HashSet<prLotQty>();
            prMarketPlaceProducts = new HashSet<prMarketPlaceProduct>();
            prProductDimSetContents = new HashSet<prProductDimSetContent>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType3 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType4 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType5 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType6 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType7 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType8 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType9 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemDimType10 { get; set; }

        [Required]
        public short SortOrder { get; set; }

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

        public virtual ICollection<prItemVariant> prItemVariants { get; set; }
        public virtual ICollection<prLotQty> prLotQtys { get; set; }
        public virtual ICollection<prMarketPlaceProduct> prMarketPlaceProducts { get; set; }
        public virtual ICollection<prProductDimSetContent> prProductDimSetContents { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
