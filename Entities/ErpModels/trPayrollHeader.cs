using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollHeader")]
    public partial class trPayrollHeader
    {
        public trPayrollHeader()
        {
            rpRegisteredEmailForPayrollSendStatuss = new HashSet<rpRegisteredEmailForPayrollSendStatus>();
            trPayrollLines = new HashSet<trPayrollLine>();
        }

        [Key]
        [Required]
        public Guid PayrollHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public byte SortOrder { get; set; }

        [Required]
        public byte PayrollTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WagePlanTypeCode { get; set; }

        public bool? IsPlanned { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdWagePlanType cdWagePlanType { get; set; }
        public virtual cdPayrollType cdPayrollType { get; set; }

        public virtual ICollection<rpRegisteredEmailForPayrollSendStatus> rpRegisteredEmailForPayrollSendStatuss { get; set; }
        public virtual ICollection<trPayrollLine> trPayrollLines { get; set; }
    }
}
