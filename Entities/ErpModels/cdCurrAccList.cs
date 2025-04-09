using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrAccList")]
    public partial class cdCurrAccList
    {
        public cdCurrAccList()
        {
            cdCurrAccListDescs = new HashSet<cdCurrAccListDesc>();
            dfSMSForCustomerRelationships = new HashSet<dfSMSForCustomerRelationship>();
            prCurrAccListContents = new HashSet<prCurrAccListContent>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccListCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public double ControlGroupRate { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public DateTime ExpireStartDate { get; set; }

        [Required]
        public DateTime ExpireEndDate { get; set; }

        // Navigation Properties
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }

        public virtual ICollection<cdCurrAccListDesc> cdCurrAccListDescs { get; set; }
        public virtual ICollection<dfSMSForCustomerRelationship> dfSMSForCustomerRelationships { get; set; }
        public virtual ICollection<prCurrAccListContent> prCurrAccListContents { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
    }
}
