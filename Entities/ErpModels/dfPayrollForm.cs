using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPayrollForm")]
    public partial class dfPayrollForm
    {
        public dfPayrollForm()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte FormType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FormName1 { get; set; }

        [Required]
        public byte CopyCount1 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FormName2 { get; set; }

        [Required]
        public byte CopyCount2 { get; set; }

        [Required]
        public bool AskCopyCount { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

    }
}
