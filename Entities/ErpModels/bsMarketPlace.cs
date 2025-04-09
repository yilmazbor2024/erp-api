using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsMarketPlace")]
    public partial class bsMarketPlace
    {
        public bsMarketPlace()
        {
            dfAttTypesForMarketPlaceCategorys = new HashSet<dfAttTypesForMarketPlaceCategory>();
            dfMarketPlaceParameterss = new HashSet<dfMarketPlaceParameters>();
            prCurrAccExtendedPropertiess = new HashSet<prCurrAccExtendedProperties>();
            prDeliveryCompanyMarketPlaceMappings = new HashSet<prDeliveryCompanyMarketPlaceMapping>();
            prMarketPlaceCategoryAttConverts = new HashSet<prMarketPlaceCategoryAttConvert>();
            prMarketPlaceCategoryAttTypes = new HashSet<prMarketPlaceCategoryAttType>();
            prMarketPlaceCategoryAttTypeConverts = new HashSet<prMarketPlaceCategoryAttTypeConvert>();
            prMarketPlaceCategoryConverts = new HashSet<prMarketPlaceCategoryConvert>();
            prMarketPlaceCreditCardMappingss = new HashSet<prMarketPlaceCreditCardMappings>();
            prMarketPlaceItemVariants = new HashSet<prMarketPlaceItemVariant>();
            prMarketPlaceProducts = new HashSet<prMarketPlaceProduct>();
            prMarketPlaceProductHierarchyConverts = new HashSet<prMarketPlaceProductHierarchyConvert>();
            prMarketPlaceProductInformations = new HashSet<prMarketPlaceProductInformation>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MarketPlaceDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<dfAttTypesForMarketPlaceCategory> dfAttTypesForMarketPlaceCategorys { get; set; }
        public virtual ICollection<dfMarketPlaceParameters> dfMarketPlaceParameterss { get; set; }
        public virtual ICollection<prCurrAccExtendedProperties> prCurrAccExtendedPropertiess { get; set; }
        public virtual ICollection<prDeliveryCompanyMarketPlaceMapping> prDeliveryCompanyMarketPlaceMappings { get; set; }
        public virtual ICollection<prMarketPlaceCategoryAttConvert> prMarketPlaceCategoryAttConverts { get; set; }
        public virtual ICollection<prMarketPlaceCategoryAttType> prMarketPlaceCategoryAttTypes { get; set; }
        public virtual ICollection<prMarketPlaceCategoryAttTypeConvert> prMarketPlaceCategoryAttTypeConverts { get; set; }
        public virtual ICollection<prMarketPlaceCategoryConvert> prMarketPlaceCategoryConverts { get; set; }
        public virtual ICollection<prMarketPlaceCreditCardMappings> prMarketPlaceCreditCardMappingss { get; set; }
        public virtual ICollection<prMarketPlaceItemVariant> prMarketPlaceItemVariants { get; set; }
        public virtual ICollection<prMarketPlaceProduct> prMarketPlaceProducts { get; set; }
        public virtual ICollection<prMarketPlaceProductHierarchyConvert> prMarketPlaceProductHierarchyConverts { get; set; }
        public virtual ICollection<prMarketPlaceProductInformation> prMarketPlaceProductInformations { get; set; }
    }
}
