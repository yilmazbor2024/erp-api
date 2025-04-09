using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPermissionMarketingService")]
    public partial class cdPermissionMarketingService
    {
        public cdPermissionMarketingService()
        {
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfMobildevStoreCollectorIDs = new HashSet<dfMobildevStoreCollectorID>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PermissionMarketingServiceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PermissionMarketingServiceDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Gateway { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApiKey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Secret { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OptInCollectorID { get; set; }

        [Required]
        public byte OptInType { get; set; }

        [Required]
        public byte OptInMethod { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OptOutCollectorID { get; set; }

        [Required]
        public byte OptOutType { get; set; }

        [Required]
        public byte OptOutMethod { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SyncServiceCollectorID { get; set; }

        [Required]
        public int BrandID { get; set; }

        [Required]
        public bool SeparateSMSPermissionMarketingAndMMS { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfMobildevStoreCollectorID> dfMobildevStoreCollectorIDs { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
    }
}
