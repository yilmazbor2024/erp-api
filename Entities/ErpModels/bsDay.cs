using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDay")]
    public partial class bsDay
    {
        public bsDay()
        {
            bsDayDescs = new HashSet<bsDayDesc>();
            prStoreWorkingHourss = new HashSet<prStoreWorkingHours>();
            prTimePeriodDays = new HashSet<prTimePeriodDay>();
        }

        [Key]
        [Required]
        public byte DayCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDayDesc> bsDayDescs { get; set; }
        public virtual ICollection<prStoreWorkingHours> prStoreWorkingHourss { get; set; }
        public virtual ICollection<prTimePeriodDay> prTimePeriodDays { get; set; }
    }
}
