using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccUserWarning")]
    public partial class prCurrAccUserWarning
    {
        public prCurrAccUserWarning()
        {
        }

        [Key]
        [Required]
        public Guid CurrAccUserWarningID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UserWarningCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StoreCRMGroupCode { get; set; }

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

        // Navigation Properties
        public virtual cdStoreCRMGroup cdStoreCRMGroup { get; set; }
        public virtual cdUserWarning cdUserWarning { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
