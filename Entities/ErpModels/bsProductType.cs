using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsProductType")]
    public partial class bsProductType
    {
        public bsProductType()
        {
            bsProductTypeDescs = new HashSet<bsProductTypeDesc>();
            cdBaseMaterials = new HashSet<cdBaseMaterial>();
            cdBrands = new HashSet<cdBrand>();
            cdItems = new HashSet<cdItem>();
            cdManufacturers = new HashSet<cdManufacturer>();
            dfPosOrderOpticalProductFields = new HashSet<dfPosOrderOpticalProductField>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prInsuranceAgencyContributions = new HashSet<prInsuranceAgencyContribution>();
        }

        [Key]
        [Required]
        public byte ProductTypeCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsProductTypeDesc> bsProductTypeDescs { get; set; }
        public virtual ICollection<cdBaseMaterial> cdBaseMaterials { get; set; }
        public virtual ICollection<cdBrand> cdBrands { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdManufacturer> cdManufacturers { get; set; }
        public virtual ICollection<dfPosOrderOpticalProductField> dfPosOrderOpticalProductFields { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prInsuranceAgencyContribution> prInsuranceAgencyContributions { get; set; }
    }
}
