using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrEmployeeJobTitle")]
    public partial class hrEmployeeJobTitle
    {
        public hrEmployeeJobTitle()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public DateTime StartDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

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
        public virtual hrEmployeePayrollProfile hrEmployeePayrollProfile { get; set; }
        public virtual cdJobTitle cdJobTitle { get; set; }

    }
}
