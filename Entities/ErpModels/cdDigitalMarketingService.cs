using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDigitalMarketingService")]
    public partial class cdDigitalMarketingService
    {
        public cdDigitalMarketingService()
        {
            cdDigitalMarketingServiceDescs = new HashSet<cdDigitalMarketingServiceDesc>();
            dfCompanyDigitalMarketingServiceAdresss = new HashSet<dfCompanyDigitalMarketingServiceAdress>();
            dfStoreDigitalMarketingServices = new HashSet<dfStoreDigitalMarketingService>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DigitalMarketingServiceCode { get; set; }

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

        public virtual ICollection<cdDigitalMarketingServiceDesc> cdDigitalMarketingServiceDescs { get; set; }
        public virtual ICollection<dfCompanyDigitalMarketingServiceAdress> dfCompanyDigitalMarketingServiceAdresss { get; set; }
        public virtual ICollection<dfStoreDigitalMarketingService> dfStoreDigitalMarketingServices { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
    }
}
