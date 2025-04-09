using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEmployeeRecordType")]
    public partial class cdEmployeeRecordType
    {
        public cdEmployeeRecordType()
        {
            cdEmployeeRecordTypeDescs = new HashSet<cdEmployeeRecordTypeDesc>();
            prEmployeeRecords = new HashSet<prEmployeeRecord>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmployeeRecordTypeCode { get; set; }

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

        public virtual ICollection<cdEmployeeRecordTypeDesc> cdEmployeeRecordTypeDescs { get; set; }
        public virtual ICollection<prEmployeeRecord> prEmployeeRecords { get; set; }
    }
}
