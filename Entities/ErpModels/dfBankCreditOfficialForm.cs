using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfBankCreditOfficialForm")]
    public partial class dfBankCreditOfficialForm
    {
        public dfBankCreditOfficialForm()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }

    }
}
