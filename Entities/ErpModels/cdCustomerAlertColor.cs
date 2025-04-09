using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerAlertColor")]
    public partial class cdCustomerAlertColor
    {
        public cdCustomerAlertColor()
        {
            cdCustomerAlertColorDescs = new HashSet<cdCustomerAlertColorDesc>();
            cdCustomerCRMGroups = new HashSet<cdCustomerCRMGroup>();
            prCustomerCompanyBrandAttributes = new HashSet<prCustomerCompanyBrandAttribute>();
        }

        [Key]
        [Required]
        public byte CustomerAlertColorCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorHex { get; set; }

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

        public virtual ICollection<cdCustomerAlertColorDesc> cdCustomerAlertColorDescs { get; set; }
        public virtual ICollection<cdCustomerCRMGroup> cdCustomerCRMGroups { get; set; }
        public virtual ICollection<prCustomerCompanyBrandAttribute> prCustomerCompanyBrandAttributes { get; set; }
    }
}
