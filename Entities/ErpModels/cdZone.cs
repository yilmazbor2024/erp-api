using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdZone")]
    public partial class cdZone
    {
        public cdZone()
        {
            cdCompanys = new HashSet<cdCompany>();
            cdZoneDescs = new HashSet<cdZoneDesc>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ZoneCode { get; set; }

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

        public virtual ICollection<cdCompany> cdCompanys { get; set; }
        public virtual ICollection<cdZoneDesc> cdZoneDescs { get; set; }
    }
}
