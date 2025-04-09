using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEasyStartupSteps")]
    public partial class bsEasyStartupSteps
    {
        public bsEasyStartupSteps()
        {
            bsEasyStartupStepsDescs = new HashSet<bsEasyStartupStepsDesc>();
            prEasyStartupCommentss = new HashSet<prEasyStartupComments>();
            prEasyStartupNotess = new HashSet<prEasyStartupNotes>();
        }

        [Key]
        [Required]
        public int EasyStartupStepCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEasyStartupStepsDesc> bsEasyStartupStepsDescs { get; set; }
        public virtual ICollection<prEasyStartupComments> prEasyStartupCommentss { get; set; }
        public virtual ICollection<prEasyStartupNotes> prEasyStartupNotess { get; set; }
    }
}
