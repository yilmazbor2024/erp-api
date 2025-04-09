using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUserWarning")]
    public partial class cdUserWarning
    {
        public cdUserWarning()
        {
            cdUserWarningDescs = new HashSet<cdUserWarningDesc>();
            prCurrAccUserWarnings = new HashSet<prCurrAccUserWarning>();
            prGLAccUserWarnings = new HashSet<prGLAccUserWarning>();
            prPresentCardActivationStepss = new HashSet<prPresentCardActivationSteps>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UserWarningCode { get; set; }

        [Required]
        public bool UseOnOpenCard { get; set; }

        [Required]
        public bool UseOnOpenTransaction { get; set; }

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

        public virtual ICollection<cdUserWarningDesc> cdUserWarningDescs { get; set; }
        public virtual ICollection<prCurrAccUserWarning> prCurrAccUserWarnings { get; set; }
        public virtual ICollection<prGLAccUserWarning> prGLAccUserWarnings { get; set; }
        public virtual ICollection<prPresentCardActivationSteps> prPresentCardActivationStepss { get; set; }
    }
}
