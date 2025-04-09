using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPresentCardActivationProcess")]
    public partial class bsPresentCardActivationProcess
    {
        public bsPresentCardActivationProcess()
        {
            bsPresentCardActivationProcessDescs = new HashSet<bsPresentCardActivationProcessDesc>();
            prPresentCardActivationStepss = new HashSet<prPresentCardActivationSteps>();
        }

        [Key]
        [Required]
        public byte ActivationProcessCode { get; set; }

        [Required]
        public bool IsOptional { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPresentCardActivationProcessDesc> bsPresentCardActivationProcessDescs { get; set; }
        public virtual ICollection<prPresentCardActivationSteps> prPresentCardActivationStepss { get; set; }
    }
}
