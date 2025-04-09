using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWagePlanType")]
    public partial class cdWagePlanType
    {
        public cdWagePlanType()
        {
            cdWagePlanTypeDescs = new HashSet<cdWagePlanTypeDesc>();
            trPayrollHeaders = new HashSet<trPayrollHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WagePlanTypeCode { get; set; }

        [Required]
        public double WageIncreaseRate { get; set; }

        [Required]
        public bool GetLastYearEarnings { get; set; }

        [Required]
        public double EarningsIncreaseRate { get; set; }

        [Required]
        public bool GetLastYearsWorkDays { get; set; }

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

        public virtual ICollection<cdWagePlanTypeDesc> cdWagePlanTypeDescs { get; set; }
        public virtual ICollection<trPayrollHeader> trPayrollHeaders { get; set; }
    }
}
