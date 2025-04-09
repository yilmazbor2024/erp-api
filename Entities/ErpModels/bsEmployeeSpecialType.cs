using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEmployeeSpecialType")]
    public partial class bsEmployeeSpecialType
    {
        public bsEmployeeSpecialType()
        {
            bsEmployeeSpecialTypeDescs = new HashSet<bsEmployeeSpecialTypeDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [Required]
        public byte EmployeeSpecialTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEmployeeSpecialTypeDesc> bsEmployeeSpecialTypeDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
