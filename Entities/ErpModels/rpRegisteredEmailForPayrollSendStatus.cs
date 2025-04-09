using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpRegisteredEmailForPayrollSendStatus")]
    public partial class rpRegisteredEmailForPayrollSendStatus
    {
        public rpRegisteredEmailForPayrollSendStatus()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public int ValidYear { get; set; }

        [Key]
        [Required]
        public int ValidMonth { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public Guid PayrollHeaderID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string KepMessageID { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual trPayrollHeader trPayrollHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
