using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPriceListHeader")]
    public partial class trPriceListHeader
    {
        public trPriceListHeader()
        {
            trPriceListLines = new HashSet<trPriceListLine>();
        }

        [Key]
        [Required]
        public Guid PriceListHeaderID { get; set; }

        [Required]
        public object PriceListNumber { get; set; }

        [Required]
        public DateTime PriceListDate { get; set; }

        [Required]
        public TimeSpan PriceListTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceListTypeCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [Required]
        public DateTime ValidDate { get; set; }

        [Required]
        public TimeSpan ValidTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public bool IsTaxIncluded { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }
        public virtual cdPriceListType cdPriceListType { get; set; }

        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
    }
}
