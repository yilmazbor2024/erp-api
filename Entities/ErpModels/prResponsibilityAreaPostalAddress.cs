using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prResponsibilityAreaPostalAddress")]
    public partial class prResponsibilityAreaPostalAddress
    {
        public prResponsibilityAreaPostalAddress()
        {
        }

        [Key]
        [Required]
        public Guid ResponsibilityAreaPostalAddressID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ResponsibilityAreaCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

        [Required]
        public int QuarterCode { get; set; }

        [Required]
        public int StreetCode { get; set; }

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
        public virtual cdCity cdCity { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdResponsibilityArea cdResponsibilityArea { get; set; }

    }
}
