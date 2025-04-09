using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreDigitalMarketingService")]
    public partial class dfStoreDigitalMarketingService
    {
        public dfStoreDigitalMarketingService()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DigitalMarketingServiceCode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [Required]
        public bool UpdatePresentCardNumberFromService { get; set; }

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
        public virtual dfStoreDefault dfStoreDefault { get; set; }
        public virtual cdPresentCardType cdPresentCardType { get; set; }
        public virtual cdDigitalMarketingService cdDigitalMarketingService { get; set; }

    }
}
