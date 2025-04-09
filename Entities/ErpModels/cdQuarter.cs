using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdQuarter")]
    public partial class cdQuarter
    {
        public cdQuarter()
        {
            cdStreets = new HashSet<cdStreet>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

        [Key]
        [Required]
        public int QuarterCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string QuarterName { get; set; }

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
        public virtual cdDistrict cdDistrict { get; set; }

        public virtual ICollection<cdStreet> cdStreets { get; set; }
    }
}
