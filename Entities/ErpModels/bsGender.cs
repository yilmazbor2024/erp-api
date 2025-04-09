using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsGender")]
    public partial class bsGender
    {
        public bsGender()
        {
            bsGenderDescs = new HashSet<bsGenderDesc>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
            tpAgentReservationActualPaxs = new HashSet<tpAgentReservationActualPax>();
        }

        [Key]
        [Required]
        public byte GenderCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsGenderDesc> bsGenderDescs { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
        public virtual ICollection<tpAgentReservationActualPax> tpAgentReservationActualPaxs { get; set; }
    }
}
