using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollLineGarnishment")]
    public partial class trPayrollLineGarnishment
    {
        public trPayrollLineGarnishment()
        {
        }

        [Key]
        [Required]
        public Guid PayrollLineGarnishmentID { get; set; }

        [Required]
        public Guid PayrollLineID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Guid? WageGarnishmentID { get; set; }

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
        public virtual hrWageGarnishment hrWageGarnishment { get; set; }
        public virtual trPayrollLine trPayrollLine { get; set; }

    }
}
