using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTimePeriod")]
    public partial class cdTimePeriod
    {
        public cdTimePeriod()
        {
            cdTimePeriodDescs = new HashSet<cdTimePeriodDesc>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
            prTimePeriodDays = new HashSet<prTimePeriodDay>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TimePeriodCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public bool IsHaveDayFilter { get; set; }

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

        public virtual ICollection<cdTimePeriodDesc> cdTimePeriodDescs { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
        public virtual ICollection<prTimePeriodDay> prTimePeriodDays { get; set; }
    }
}
