using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPresentCardType")]
    public partial class cdPresentCardType
    {
        public cdPresentCardType()
        {
            cdCustomerCRMGroups = new HashSet<cdCustomerCRMGroup>();
            cdPresentCardTypeDescs = new HashSet<cdPresentCardTypeDesc>();
            dfChippins = new HashSet<dfChippin>();
            dfStoreDigitalMarketingServices = new HashSet<dfStoreDigitalMarketingService>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
            prPresentCardValidCardTypess = new HashSet<prPresentCardValidCardTypes>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [Required]
        public bool UsePOS { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StoreCRMGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public bool DisablePresentCardWhenCommAddressChanged { get; set; }

        [Required]
        public bool UseConditionalPresentCardActivation { get; set; }

        [Required]
        public bool AutoGenerateCardNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PrefixCode { get; set; }

        [Required]
        public byte DigitCount { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

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

        // Navigation Properties
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdStoreCRMGroup cdStoreCRMGroup { get; set; }

        public virtual ICollection<cdCustomerCRMGroup> cdCustomerCRMGroups { get; set; }
        public virtual ICollection<cdPresentCardTypeDesc> cdPresentCardTypeDescs { get; set; }
        public virtual ICollection<dfChippin> dfChippins { get; set; }
        public virtual ICollection<dfStoreDigitalMarketingService> dfStoreDigitalMarketingServices { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
        public virtual ICollection<prPresentCardValidCardTypes> prPresentCardValidCardTypess { get; set; }
    }
}
