using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPresentCardActivationSteps")]
    public partial class prPresentCardActivationSteps
    {
        public prPresentCardActivationSteps()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [Key]
        [Required]
        public byte ActivationProcessCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConditionTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UserWarningCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NegotaryUserWarningCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Originator { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

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
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual bsPresentCardActivationProcess bsPresentCardActivationProcess { get; set; }
        public virtual cdUserWarning cdUserWarning { get; set; }
        public virtual cdConditionType cdConditionType { get; set; }

    }
}
