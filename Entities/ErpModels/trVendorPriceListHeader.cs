using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trVendorPriceListHeader")]
    public partial class trVendorPriceListHeader
    {
        public trVendorPriceListHeader()
        {
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [Required]
        public Guid VendorPriceListHeaderID { get; set; }

        [Required]
        public object VendorPriceListNumber { get; set; }

        [Required]
        public DateTime VendorPriceListDate { get; set; }

        [Required]
        public TimeSpan VendorPriceListTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte VendorTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VendorCode { get; set; }

        [Required]
        public DateTime ValidDate { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
