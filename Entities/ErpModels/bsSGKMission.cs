using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSGKMission")]
    public partial class bsSGKMission
    {
        public bsSGKMission()
        {
            bsSGKMissionDescs = new HashSet<bsSGKMissionDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKMissionCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsSGKMissionDesc> bsSGKMissionDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
