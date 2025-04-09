using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSpecialDayType")]
    public partial class cdSpecialDayType
    {
        public cdSpecialDayType()
        {
            cdSpecialDayTypeDescs = new HashSet<cdSpecialDayTypeDesc>();
            prStoreSpecialDays = new HashSet<prStoreSpecialDay>();
            trCountrySpecialDays = new HashSet<trCountrySpecialDay>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SpecialDayTypeCode { get; set; }

        [Required]
        public bool IsOfficialHoliday { get; set; }

        [Required]
        public bool IsNegativeDay { get; set; }

        [Required]
        public bool IsHalfOfficialHoliday { get; set; }

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

        public virtual ICollection<cdSpecialDayTypeDesc> cdSpecialDayTypeDescs { get; set; }
        public virtual ICollection<prStoreSpecialDay> prStoreSpecialDays { get; set; }
        public virtual ICollection<trCountrySpecialDay> trCountrySpecialDays { get; set; }
    }
}
