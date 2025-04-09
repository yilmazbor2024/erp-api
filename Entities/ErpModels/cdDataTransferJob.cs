using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDataTransferJob")]
    public partial class cdDataTransferJob
    {
        public cdDataTransferJob()
        {
            prDataTransferJobClientss = new HashSet<prDataTransferJobClients>();
            prDataTransferJobSchedules = new HashSet<prDataTransferJobSchedule>();
        }

        [Key]
        [Required]
        public int DataTransferJobID { get; set; }

        [Required]
        public int DataTransferTemplateId { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DataTransferJobCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string DataTransferJobDescription { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

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
        public virtual cdDataTransferTemplate cdDataTransferTemplate { get; set; }

        public virtual ICollection<prDataTransferJobClients> prDataTransferJobClientss { get; set; }
        public virtual ICollection<prDataTransferJobSchedule> prDataTransferJobSchedules { get; set; }
    }
}
