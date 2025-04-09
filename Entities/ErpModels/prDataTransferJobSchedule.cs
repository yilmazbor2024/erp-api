using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDataTransferJobSchedule")]
    public partial class prDataTransferJobSchedule
    {
        public prDataTransferJobSchedule()
        {
        }

        [Key]
        [Required]
        public int DataTransferJobId { get; set; }

        [Key]
        [Required]
        public int ScheduleId { get; set; }

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
        public virtual cdDataTransferJob cdDataTransferJob { get; set; }
        public virtual cdDataTransferSchedule cdDataTransferSchedule { get; set; }

    }
}
