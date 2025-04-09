using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSGKInsuaranceType")]
    public partial class bsSGKInsuaranceType
    {
        public bsSGKInsuaranceType()
        {
            bsSGKInsuaranceTypeDescs = new HashSet<bsSGKInsuaranceTypeDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKInsuaranceTypeCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsSGKInsuaranceTypeDesc> bsSGKInsuaranceTypeDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
