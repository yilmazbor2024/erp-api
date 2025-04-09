using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeRecord")]
    public partial class prEmployeeRecord
    {
        public prEmployeeRecord()
        {
        }

        [Key]
        [Required]
        public Guid EmployeeRecordID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmployeeRecordTypeCode { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
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

        // Navigation Properties
        public virtual cdEmployeeRecordType cdEmployeeRecordType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
