using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdConditionType")]
    public partial class cdConditionType
    {
        public cdConditionType()
        {
            cdConditionTypeDescs = new HashSet<cdConditionTypeDesc>();
            dfRetailCustomerConditionalRequiredFieldss = new HashSet<dfRetailCustomerConditionalRequiredFields>();
            prPresentCardActivationStepss = new HashSet<prPresentCardActivationSteps>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConditionTypeCode { get; set; }

        [Required]
        public byte SortOrder { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ControlSpName { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object CashierMessage { get; set; }

        [Required]
        public bool IsForPresentCardActivation { get; set; }

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

        public virtual ICollection<cdConditionTypeDesc> cdConditionTypeDescs { get; set; }
        public virtual ICollection<dfRetailCustomerConditionalRequiredFields> dfRetailCustomerConditionalRequiredFieldss { get; set; }
        public virtual ICollection<prPresentCardActivationSteps> prPresentCardActivationStepss { get; set; }
    }
}
