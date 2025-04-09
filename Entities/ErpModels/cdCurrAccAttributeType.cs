using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrAccAttributeType")]
    public partial class cdCurrAccAttributeType
    {
        public cdCurrAccAttributeType()
        {
            cdCurrAccAttributes = new HashSet<cdCurrAccAttribute>();
            cdCurrAccAttributeTypeDescs = new HashSet<cdCurrAccAttributeTypeDesc>();
            dfPDCCurrAccAttributes = new HashSet<dfPDCCurrAccAttribute>();
            dfPDCCustomerCompanyBrandAttributes = new HashSet<dfPDCCustomerCompanyBrandAttribute>();
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool UseReports { get; set; }

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
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        public virtual ICollection<cdCurrAccAttribute> cdCurrAccAttributes { get; set; }
        public virtual ICollection<cdCurrAccAttributeTypeDesc> cdCurrAccAttributeTypeDescs { get; set; }
        public virtual ICollection<dfPDCCurrAccAttribute> dfPDCCurrAccAttributes { get; set; }
        public virtual ICollection<dfPDCCustomerCompanyBrandAttribute> dfPDCCustomerCompanyBrandAttributes { get; set; }
    }
}
